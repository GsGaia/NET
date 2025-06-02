using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gaia.Domain.Entity;

namespace Gaia.Infrastructure.Mappings;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            builder.ToTable("USERS");

            builder.HasKey(u => u.IdUser);

            builder.Property(u => u.IdUser)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(u => u.CreationDate)
                .IsRequired();

            builder.Property(u => u.Active)
                .IsRequired();

            builder.Property(u => u.TypeUser)
                .IsRequired()
                .HasConversion<int>(); 

            builder.HasMany(u => u.Requestions)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
