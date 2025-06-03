using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gaia.Infrastructure.Context
{

    public class DbOracle : DbContext
    {
        public DbOracle(DbContextOptions<DbOracle> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Requestion> Requestions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Accident> Accidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica configurações da entidade normalmente
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbOracle).Assembly);

            // Define relacionamentos entre Requestion e Location/User
            modelBuilder.Entity<Requestion>()
                .HasOne(r => r.Location)
                .WithMany(l => l.Requestions)
                .HasForeignKey(r => r.IdLocation)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Requestion>().HasOne(r => r.User).WithMany() // ou WithMany(u => u.Requestions), se mapeado no User.HasForeignKey(r => r.IdUser).OnDelete(DeleteBehavior.Restrict);

            // Configuração global para converter bool -> int (0/1)
            var boolToIntConverter = new ValueConverter<bool, int>(
                v => v ? 1 : 0,
                v => v == 1);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var boolProperties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(bool));

                foreach (var prop in boolProperties)
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(prop.Name)
                        .HasConversion(boolToIntConverter);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}