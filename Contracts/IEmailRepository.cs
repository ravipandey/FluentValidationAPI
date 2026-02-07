public interface IEmailRepository
{
    Task<Email?> GetByIdAsync(int id);
    Task AddAsync(Email email);
    Task<IEnumerable<Email>> GetAllAsync();
}
```

**Implementations:**

```csharp