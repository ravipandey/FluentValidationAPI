using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using MailKit.Security;
using FluentValidationAPI.Data;

namespace FluentValidationAPI
{
    public class EmailProcess : IEmailProcess
    {
        private readonly AppDbContext _appDbContext;
        public EmailProcess(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task EmailExtract()
        {
            string saveFolderPath = "Email/Attachments/";
            Directory.CreateDirectory(saveFolderPath);
            //await ReceiveEmails("ravipandey.ciitm@outlook.com", "pcerpnwhxrbxgcvf");
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, MailKit.Security.SecureSocketOptions.SslOnConnect);
                client.Authenticate("ravipandey.ciitm@gmail.com", "tzwviqljbvyrefta");

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                foreach (var uid in inbox.Search(MailKit.Search.SearchQuery.FromContains("no-reply-account-statement@reportsmailer.zerodha.net")))
                {
                    var message = inbox.GetMessage(uid);

                    // Extract subject, body, sender, etc.
                    string subject = message.Subject;
                    string bodyText = message.TextBody ?? message.HtmlBody;
                    string from = message.From.ToString();

                    // Save to DB (example below)
                    int emailId = await SaveEmailToDatabase(subject, bodyText, message.Date, from);                    

                    foreach (var attachment in message.Attachments.OfType<MimePart>())
                    {
                        // Ensure a valid filename
                        var fileName = attachment.FileName;
                        if (string.IsNullOrEmpty(fileName))
                            fileName = $"unnamed-attachment-{Path.GetRandomFileName()}";

                        // Combine the folder path and file name
                        var filePath = Path.Combine(saveFolderPath, fileName);

                        // Save the attachment to the physical location using a FileStream
                        using (var stream = File.Create(filePath))
                        {
                            await attachment.Content.DecodeToAsync(stream);
                        }
                        Console.WriteLine($"Saved attachment: {filePath}");

                        await SaveAttachments(emailId, fileName);
                    }
                    //await _appDbContext.SaveChangesAsync();
                }

                client.Disconnect(true);

            }
        }

        public async Task<int> SaveEmailToDatabase(string subject, string from, DateTimeOffset date, string body)
        {
            var email = new Email { subject = subject, from = from, date = date, body = body };
            _appDbContext.Emails.Add(email);
            await _appDbContext.SaveChangesAsync();
            return email.Id;
        }

        public async Task SaveAttachments(int Emailid, string FileName)
        {
            var emailAttachment = new EmailAttachment { EmailId= Emailid, FileName = FileName };
            _appDbContext.EmailAttachments.Add(emailAttachment);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
