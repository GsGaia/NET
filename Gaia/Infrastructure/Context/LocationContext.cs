using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Infrastructure.Context
{
    public class LocationContext : DbContext
    {
        public LocationContext(DbContextOptions<LocationContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocationContext).Assembly);
            base.OnModelCreating(modelBuilder);
            
            
        }
    }
}