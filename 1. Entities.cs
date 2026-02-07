
```csharp
// Models/User.cs
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}

// Models/Email.cs
public class Email
{
    public int Id { get; set; }
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string From { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
    public ICollection<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
}

// Models/EmailAttachment.cs
public class EmailAttachment
{
    public int Id { get; set; }
    public int EmailId { get; set; }
    public string FileName { get; set; } = null!;
    public Email Email { get; set; } = null!;
}
```

---
