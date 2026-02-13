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

### 4. Service Layer

**Contracts:**

```csharp