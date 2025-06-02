using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gaia.Infrastructure.Context;

public class DbOracle : DbContext
{
    public DbOracle(DbContextOptions<DbOracle> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Requestion> Requestions { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Accident> Accidents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbOracle).Assembly);
        base.OnModelCreating(modelBuilder);
        
        var boolToIntConverter = new ValueConverter<bool, int>(
            v => v ? 1 : 0,
            v => v == 1);

        modelBuilder.Entity<Location>()
            .Property(e => e.Active)
            .HasConversion(boolToIntConverter);

        modelBuilder.Entity<User>()
            .Property(e => e.Active)
            .HasConversion(boolToIntConverter);

        modelBuilder.Entity<Requestion>()
            .Property(e => e.Active)
            .HasConversion(boolToIntConverter);

        base.OnModelCreating(modelBuilder);
    }
}