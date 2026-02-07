public interface IEmailService
{
    Task<Email> SaveEmailFromMsgAsync(Stream msgFileStream, string rootAttachmentDirectory);
    Task<IEnumerable<Email>> GetAllEmailsAsync();
}
```

**Implementation:**

```csharp