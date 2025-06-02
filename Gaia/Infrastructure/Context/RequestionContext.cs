using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Infrastructure.Context;
    public class RequestionContext : DbContext
    {
        public RequestionContext(DbContextOptions<RequestionContext> options)
            : base(options)
        {
        }

        public DbSet<Requestion> Requestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RequestionContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

