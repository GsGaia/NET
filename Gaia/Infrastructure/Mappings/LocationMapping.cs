using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaia.Infrastructure.Mappings
{
    public class LocationMapping : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("LOCATION");

            builder.HasKey(l => l.IdLocation);

            builder.Property(l => l.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.StartAccident)
                .IsRequired();

            builder.Property(l => l.EndAccident)
                .IsRequired();

            builder.Property(l => l.Status)
                .IsRequired();

            builder.Property(l => l.Station)
                .IsRequired();

            builder.HasMany(l => l.Requestions)
                .WithOne(r => r.Location)
                .HasForeignKey(r => r.IdLocation);

            builder.HasMany(l => l.Accidents)
                .WithOne(a => a.Location)
                .HasForeignKey(a => a.LocationId);
        }
    }
}