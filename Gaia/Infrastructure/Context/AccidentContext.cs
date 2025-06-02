using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Infrastructure.Context
{
    public class AccidentContext : DbContext
    {
        public AccidentContext(DbContextOptions<AccidentContext> options)
            : base(options)
        {
        }

        public DbSet<Accident> Accidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccidentContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}