using Microsoft.EntityFrameworkCore;

public class CartolaDbContext : DbContext
{
    public DbSet<Time> Times { get; set; }
    public DbSet<Jogador> Jogadores { get; set; }
    public DbSet<Escalacao> Escalacoes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=cartola.db");
}
