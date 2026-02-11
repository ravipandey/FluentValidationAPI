using Microsoft.EntityFrameworkCore;
using UserApi.Models;

public class UserDbContext : DbContext {
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }
}
