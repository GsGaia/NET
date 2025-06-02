using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaia.Infrastructure.Mappings
{
    public class RequestionMapping : IEntityTypeConfiguration<Requestion>
    {
        public void Configure(EntityTypeBuilder<Requestion> builder)
        {
            builder.ToTable("Requestion");

            builder.HasKey(r => r.IdRequestion);

            builder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(r => r.Description)
                .HasMaxLength(1000);

            builder.Property(r => r.Unit)
                .HasMaxLength(100);

            builder.Property(r => r.RequestDate)
                .IsRequired();

            builder.Property(r => r.Active)
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            builder.HasOne(r => r.Location)
                .WithMany()
                .HasForeignKey(r => r.LocationId);
        }
    }
}