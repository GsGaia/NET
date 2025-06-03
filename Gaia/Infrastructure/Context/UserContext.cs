using Gaia.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Infrastructure.Context;

public class UserContext :  DbContext
{
    public UserContext(DbContextOptions<UserContext> options):  base(options){ }

    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}