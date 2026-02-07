
**Contracts:**

```csharp
// Contracts/IUserRepository.cs
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
}

// Contracts/IEmailRepository.cs
public interface IEmailRepository
{
    Task<Email?> GetByIdAsync(int id);
    Task AddAsync(Email email);
    Task<IEnumerable<Email>> GetAllAsync();
}
```

**Implementations:**

```csharp
// Repositories/UserRepository.cs
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;

    public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}

// Repositories/EmailRepository.cs
public class EmailRepository : IEmailRepository
{
    private readonly AppDbContext _context;
    public EmailRepository(AppDbContext context) => _context = context;

    public async Task<Email?> GetByIdAsync(int id) =>
        await _context.Emails.Include(e => e.Attachments).FirstOrDefaultAsync(e => e.Id == id);

    public async Task AddAsync(Email email)
    {
        _context.Emails.Add(email);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Email>> GetAllAsync() =>
        await _context.Emails.Include(e => e.Attachments).ToListAsync();
}
```

---
