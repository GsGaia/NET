using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gaia.Domain.Entity;

namespace Gaia.Infrastructure.Mappings;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            builder.ToTable("Users");

            builder.HasKey(u => u.idUser);

            builder.Property(u => u.idUser)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(u => u.creationDate)
                .IsRequired();

            builder.Property(u => u.active)
                .IsRequired();

            builder.Property(u => u.typeUser)
                .IsRequired()
                .HasConversion<int>(); 

            builder.HasMany(u => u.requestions)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
