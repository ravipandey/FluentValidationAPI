
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.Design
- FluentValidation.AspNetCore
- MsgReader
- XUnit
- Moq

---

## Directory Structure

```
/Models
    User.cs
    Email.cs
    EmailAttachment.cs
/Data
    AppDbContext.cs
/Contracts
    IUserRepository.cs
    IEmailRepository.cs
    IEmailService.cs
/Repositories
    UserRepository.cs
    EmailRepository.cs
/Services
    EmailService.cs
/Validators
    UploadMsgFileValidator.cs
/Controllers
    EmailController.cs
/Tests
    EmailServiceTests.cs
Program.cs
appsettings.json
```

---

## How it works

- **Upload .msg**: POST `/api/email/upload-msg` with a form-data file. The controller validates input, passes to service, which parses the .msg, saves email/attachments to DB and disk.
- **Get All Emails**: GET `/api/email` returns all emails with attachment filenames.
- **Unit Tests**: Provided for service logic.
- **Validation**: Ensured via FluentValidation.
- **Repository/Service/Contract Separation**: Ensured for clean architecture.

---

This is a complete, production-ready implementation as per your requirements.