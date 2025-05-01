using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services.AddDbContext<CartolaDbContext>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();

app.MapGet("/times", async (CartolaDbContext db) => await db.Times.ToListAsync());

app.MapPost(
    "/times",
    async (CartolaDbContext db, Time time) =>
    {
        db.Times.Add(time);
        await db.SaveChangesAsync();
        return Results.Created($"/times/{time.Id}", time);
    }
);

app.Run();
