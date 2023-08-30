using FluentValidation;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator:AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .MaximumLength(80).WithMessage("{PropertyName} should not excede {MaxLenght} characteres");

            RuleFor(p => p.LastName).NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .MaximumLength(80).WithMessage("{PropertyName} should not excede {MaxLenght} characteres");


            RuleFor(p => p.Email).NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .EmailAddress().WithMessage("\"{PropertyName} must be a Email valid")
                .MaximumLength(100).WithMessage("{PropertyName} should not excede {MaxLenght} characteres");

            RuleFor(p => p.UserName).NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .MaximumLength(10).WithMessage("{PropertyName} should not excede {MaxLenght} characteres");

            RuleFor(p => p.Password).NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .MaximumLength(15).WithMessage("{PropertyName} should not excede {MaxLenght} characteres");

            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .MaximumLength(15).WithMessage("{PropertyName} should not excede {MaxLenght} characteres")
                .Equal(p => p.Password).WithMessage("{PropertyName} should be equal to Password");
        }
    }
}
