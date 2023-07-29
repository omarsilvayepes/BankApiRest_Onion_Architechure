using FluentValidation;

namespace Application.Features.Commands.DeleteClientCommand
{
    public class DeleteClientCommandValidator: AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("{PropertyName} Can not be empty");
        }
    }
}
