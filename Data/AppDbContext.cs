public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Email> Emails => Set<Email>();
    public DbSet<EmailAttachment> EmailAttachments => Set<EmailAttachment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Email>()
            .HasMany(e => e.Attachments)
            .WithOne(a => a.Email)
            .HasForeignKey(a => a.EmailId);
    }
}
```

---

### 3. Repository Layer

**Contracts:**

```csharp