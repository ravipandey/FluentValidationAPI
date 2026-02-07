
**Contracts:**

```csharp
// Contracts/IEmailService.cs
public interface IEmailService
{
    Task<Email> SaveEmailFromMsgAsync(Stream msgFileStream, string rootAttachmentDirectory);
    Task<IEnumerable<Email>> GetAllEmailsAsync();
}
```

**Implementation:**

```csharp
// Services/EmailService.cs
using MsgReader.Outlook; // Install MsgReader NuGet
public class EmailService : IEmailService
{
    private readonly IEmailRepository _emailRepository;
    private readonly IWebHostEnvironment _env;

    public EmailService(IEmailRepository emailRepository, IWebHostEnvironment env)
    {
        _emailRepository = emailRepository;
        _env = env;
    }

    public async Task<Email> SaveEmailFromMsgAsync(Stream msgFileStream, string rootAttachmentDirectory)
    {
        // Use MsgReader to parse .msg file
        var tempFile = Path.GetTempFileName();
        using (var fs = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
        {
            await msgFileStream.CopyToAsync(fs);
        }

        var message = new Storage.Message(tempFile);
        var email = new Email
        {
            Subject = message.Subject ?? "",
            Body = message.BodyText ?? "",
            From = message.Sender?.Email ?? "",
            Date = message.SentOn ?? DateTimeOffset.UtcNow
        };

        // Save attachments
        foreach (var attachment in message.Attachments)
        {
            var fileName = Path.Combine(rootAttachmentDirectory, attachment.FileName);
            File.WriteAllBytes(fileName, attachment.Data);
            email.Attachments.Add(new EmailAttachment { FileName = attachment.FileName });
        }

        await _emailRepository.AddAsync(email);

        File.Delete(tempFile);
        return email;
    }

    public async Task<IEnumerable<Email>> GetAllEmailsAsync() => await _emailRepository.GetAllAsync();
}
```

---
