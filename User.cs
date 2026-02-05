using FluentValidation;
using FluentValidationAPI.Data;

namespace FluentValidationAPI
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator(IUserRepository userRepository) 
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("User name cannot be empty.");

            RuleFor(x => x.Name).MustAsync(async (model,name, cancellationToken) => !await userRepository.NameExistsAsync(model,name, cancellationToken))
            .WithMessage("A product with this name already exists.");

            RuleFor(x => x.Name).Length(0, 10);

            RuleFor(x=>x.Email).NotEmpty();
        }
    }
}
