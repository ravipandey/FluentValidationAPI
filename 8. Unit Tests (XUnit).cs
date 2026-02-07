
```csharp
// Tests/EmailServiceTests.cs
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
