using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Configuração do DbContext com provider e connection string
builder.Services.AddDbContext<CartolaDbContext>(options =>
{
    options.UseSqlite("Data Source=cartola.db");
    options.EnableDetailedErrors();
});

var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();

// *******************************
// Endpoints Times
// *******************************
app.MapGet("/times", async (CartolaDbContext db) => await db.Times.ToListAsync());

app.MapGet(
    "/times/{id}",
    async (int id, CartolaDbContext db) =>
    {
        var time = await db.Times.FindAsync(id);
        return time is not null ? Results.Ok(time) : Results.NotFound();
    }
);

app.MapPost(
    "/times",
    async (Time time, CartolaDbContext db) =>
    {
        db.Times.Add(time);
        await db.SaveChangesAsync();
        return Results.Created($"/times/{time.Id}", time);
    }
);

app.MapPut(
    "/times/{id}",
    async (int id, Time input, CartolaDbContext db) =>
    {
        var time = await db.Times.FindAsync(id);
        if (time is null)
            return Results.NotFound();
        time.Nome = input.Nome;
        time.GolsFeitos = input.GolsFeitos;
        time.GolsSofridos = input.GolsSofridos;
        time.Vitorias = input.Vitorias;
        time.Derrotas = input.Derrotas;
        time.Empates = input.Empates;
        await db.SaveChangesAsync();
        return Results.Ok(time);
    }
);

app.MapDelete(
    "/times/{id}",
    async (int id, CartolaDbContext db) =>
    {
        var time = await db.Times.FindAsync(id);
        if (time is null)
            return Results.NotFound();
        db.Times.Remove(time);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
);

// *******************************
// Endpoints Jogadores
// *******************************
app.MapGet("/jogadores", async (CartolaDbContext db) => await db.Jogadores.ToListAsync());

app.MapGet(
    "/jogadores/{id}",
    async (int id, CartolaDbContext db) =>
    {
        var jogador = await db.Jogadores.FindAsync(id);
        return jogador is not null ? Results.Ok(jogador) : Results.NotFound();
    }
);

app.MapPost(
    "/jogadores",
    async (Jogador jogador, CartolaDbContext db) =>
    {
        db.Jogadores.Add(jogador);
        await db.SaveChangesAsync();
        return Results.Created($"/jogadores/{jogador.Id}", jogador);
    }
);

app.MapPut(
    "/jogadores/{id}",
    async (int id, Jogador input, CartolaDbContext db) =>
    {
        var jogador = await db.Jogadores.FindAsync(id);
        if (jogador is null)
            return Results.NotFound();
        jogador.Nome = input.Nome;
        jogador.Tecnica = input.Tecnica;
        jogador.Defesa = input.Defesa;
        jogador.PrecisaoFinalizacao = input.PrecisaoFinalizacao;
        jogador.PrecisaoPasse = input.PrecisaoPasse;
        jogador.AptidaoFisica = input.AptidaoFisica;
        jogador.Agilidade = input.Agilidade;
        jogador.Mentalidade = input.Mentalidade;
        jogador.Posicao = input.Posicao;
        jogador.TimeId = input.TimeId;
        await db.SaveChangesAsync();
        return Results.Ok(jogador);
    }
);

app.MapDelete(
    "/jogadores/{id}",
    async (int id, CartolaDbContext db) =>
    {
        var jogador = await db.Jogadores.FindAsync(id);
        if (jogador is null)
            return Results.NotFound();
        db.Jogadores.Remove(jogador);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
);

// *******************************
// Endpoints Partidas
// *******************************
app.MapGet("/partidas", async (CartolaDbContext db) => await db.Partidas.ToListAsync());

app.MapGet(
    "/partidas/{id}",
    async (int id, CartolaDbContext db) =>
    {
        var partida = await db.Partidas.FindAsync(id);
        return partida is not null ? Results.Ok(partida) : Results.NotFound();
    }
);

app.MapPost(
    "/partidas",
    async (Partida partida, CartolaDbContext db) =>
    {
        db.Partidas.Add(partida);
        await db.SaveChangesAsync();
        return Results.Created($"/partidas/{partida.Id}", partida);
    }
);

app.MapPut(
    "/partidas/{id}",
    async (int id, Partida input, CartolaDbContext db) =>
    {
        var partida = await db.Partidas.FindAsync(id);
        if (partida is null)
            return Results.NotFound();
        partida.PlacarCasa = input.PlacarCasa;
        partida.PlacarFora = input.PlacarFora;
        partida.Data = input.Data;
        partida.Finalizada = input.Finalizada;
        partida.TorneioId = input.TorneioId;
        await db.SaveChangesAsync();
        return Results.Ok(partida);
    }
);

app.MapDelete(
    "/partidas/{id}",
    async (int id, CartolaDbContext db) =>
    {
        var partida = await db.Partidas.FindAsync(id);
        if (partida is null)
            return Results.NotFound();
        db.Partidas.Remove(partida);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
);

// *******************************
// Endpoints Torneios
// *******************************
app.MapGet("/torneios", async (CartolaDbContext db) => await db.Torneios.ToListAsync());

app.MapGet(
    "/torneios/{id}",
    async (int id, CartolaDbContext db) =>
    {
        var torneio = await db.Torneios.FindAsync(id);
        return torneio is not null ? Results.Ok(torneio) : Results.NotFound();
    }
);

app.MapPost(
    "/torneios",
    async (Torneio torneio, CartolaDbContext db) =>
    {
        db.Torneios.Add(torneio);
        await db.SaveChangesAsync();
        return Results.Created($"/torneios/{torneio.Id}", torneio);
    }
);

app.MapPut(
    "/torneios/{id}",
    async (int id, Torneio input, CartolaDbContext db) =>
    {
        var torneio = await db.Torneios.FindAsync(id);
        if (torneio is null)
            return Results.NotFound();
        torneio.Nome = input.Nome;
        torneio.Tipo = input.Tipo;
        torneio.Status = input.Status;
        await db.SaveChangesAsync();
        return Results.Ok(torneio);
    }
);

app.MapDelete(
    "/torneios/{id}",
    async (int id, CartolaDbContext db) =>
    {
        var torneio = await db.Torneios.FindAsync(id);
        if (torneio is null)
            return Results.NotFound();
        db.Torneios.Remove(torneio);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
);

app.Run();
