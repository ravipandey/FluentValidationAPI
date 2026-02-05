namespace FluentValidationAPI
{
    public class Email
    {
        public int Id { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string from { get; set; }
        public DateTimeOffset date { get; set; }
    }

    public class EmailAttachment
    {
        public int Id { get; set; }
        public int EmailId { get; set; }
        public string FileName { get; set; }

    }
}
