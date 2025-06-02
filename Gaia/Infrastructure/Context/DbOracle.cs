using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Infrastructure.Context;

public class DbPostgresql : DbContext
{
    public DbPostgresql(DbContextOptions<DbPostgresql> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Requestion> Requestions { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Accident> Accidents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbPostgresql).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}