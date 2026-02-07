public class EmailAttachment
{
    public int Id { get; set; }
    public int EmailId { get; set; }
    public string FileName { get; set; } = null!;
    public Email Email { get; set; } = null!;
}
```

---

### 2. DbContext

```csharp