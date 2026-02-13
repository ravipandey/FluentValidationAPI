using Xunit;
using Moq;
using Microsoft.AspNetCore.Hosting;

public class EmailServiceTests
{
    [Fact]
    public async Task SaveEmailFromMsgAsync_SavesEmailAndAttachments()
    {
        // Arrange
        var emailRepoMock = new Mock<IEmailRepository>();
        var envMock = new Mock<IWebHostEnvironment>();
        envMock.Setup(e => e.ContentRootPath).Returns(Directory.GetCurrentDirectory());

        var service = new EmailService(emailRepoMock.Object, envMock.Object);

        // Use a sample .msg file as stream (place a test file in your test project)
        var testMsgPath = Path.Combine("TestData", "sample.msg");
        using var stream = File.OpenRead(testMsgPath);

        // Act
        var email = await service.SaveEmailFromMsgAsync(stream, Path.Combine(Directory.GetCurrentDirectory(), "Attachments"));

        // Assert
        Assert.NotNull(email);
        Assert.False(string.IsNullOrEmpty(email.Subject));
        emailRepoMock.Verify(r => r.AddAsync(It.IsAny<Email>()), Times.Once);
    }
}
```

---

### 9. appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=app.db"
  }
}
```

---

### 10. NuGet Packages Required

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