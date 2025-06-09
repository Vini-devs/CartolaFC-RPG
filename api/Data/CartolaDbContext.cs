using Microsoft.EntityFrameworkCore;

public class CartolaDbContext : DbContext
{
    public CartolaDbContext(DbContextOptions<CartolaDbContext> options)
        : base(options) { }

    public DbSet<Time> Times { get; set; }
    public DbSet<Jogador> Jogadores { get; set; }
    public DbSet<Partida> Partidas { get; set; }
    public DbSet<Torneio> Torneios { get; set; }
}
