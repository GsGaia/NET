using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaia.Infrastructure.Mappings
{
    public class AccidentMapping : IEntityTypeConfiguration<Accident>
    {
        public void Configure(EntityTypeBuilder<Accident> builder)
        {
            builder.ToTable("Accident");

            builder.HasKey(a => a.IdAccident);

            builder.Property(a => a.DateAccidentStart)
                .IsRequired();

            builder.Property(a => a.DateAccidentEnd)
                .IsRequired();

            builder.Property(a => a.TypeSeverity)
                .IsRequired();

            builder.Property(a => a.TypeAccident)
                .IsRequired();

            builder.HasOne(a => a.Location)
                .WithMany(l => l.Accidents)
                .HasForeignKey(a => a.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}