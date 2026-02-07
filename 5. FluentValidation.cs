
```csharp
// Validators/UploadMsgFileValidator.cs
using FluentValidation;

public class UploadMsgFileModel
{
    public IFormFile MsgFile { get; set; } = null!;
}

public class UploadMsgFileValidator : AbstractValidator<UploadMsgFileModel>
{
    public UploadMsgFileValidator()
    {
        RuleFor(x => x.MsgFile)
            .NotNull()
            .Must(f => f.FileName.EndsWith(".msg", StringComparison.OrdinalIgnoreCase))
            .WithMessage("File must be a .msg file");
    }
}
```

---
