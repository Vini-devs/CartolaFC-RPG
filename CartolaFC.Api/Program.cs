var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CartolaDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/times", async (CartolaDbContext db) => await db.Times.ToListAsync());
app.MapPost("/times", async (CartolaDbContext db, Time time) =>
{
    db.Times.Add(time);
    await db.SaveChangesAsync();
    return Results.Created($"/times/{time.Id}", time);
});
// ...endpoints para jogadores e escalações...

app.Run();
