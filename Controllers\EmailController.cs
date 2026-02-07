using Microsoft.AspNetCore.Mvc;
using FluentValidation;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IValidator<UploadMsgFileModel> _validator;
    private readonly IWebHostEnvironment _env;

    public EmailController(IEmailService emailService, IValidator<UploadMsgFileModel> validator, IWebHostEnvironment env)
    {
        _emailService = emailService;
        _validator = validator;
        _env = env;
    }

    [HttpPost("upload-msg")]
    public async Task<IActionResult> UploadMsg([FromForm] UploadMsgFileModel model)
    {
        var validation = await _validator.ValidateAsync(model);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);

        var rootAttachmentDirectory = Path.Combine(_env.ContentRootPath, "Attachments");
        Directory.CreateDirectory(rootAttachmentDirectory);

        using var stream = model.MsgFile.OpenReadStream();
        var email = await _emailService.SaveEmailFromMsgAsync(stream, rootAttachmentDirectory);

        return Ok(new { email.Id, email.Subject, email.From, email.Date });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var emails = await _emailService.GetAllEmailsAsync();
        return Ok(emails.Select(e => new
        {
            e.Id,
            e.Subject,
            e.From,
            e.Date,
            Attachments = e.Attachments.Select(a => a.FileName)
        }));
    }
}
```

---

### 7. Dependency Injection Setup

```csharp