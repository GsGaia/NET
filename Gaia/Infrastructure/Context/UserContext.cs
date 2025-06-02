using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Infrastructure.Context;

public class UserContext :  DbContext
{
    public UserContext(DbContextOptions<UserContext> options):  base(options){ }

    public DbSet<User> Users { get; set; }
    public DbSet<Requestion>  Requestions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.idUser);
        base.OnModelCreating(modelBuilder);
    }
}