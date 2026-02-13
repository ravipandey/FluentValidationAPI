public class Email
{
    public int Id { get; set; }
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string From { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
    public ICollection<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
}
