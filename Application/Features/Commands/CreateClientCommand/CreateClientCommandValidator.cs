﻿using FluentValidation;

namespace Application.Features.Commands.CreateClientCommand
{
    public class CreateClientCommandValidator:AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .MaximumLength(80).WithMessage("{PropertyName} should not excede {MaxLenght} characteres");

            RuleFor(p => p.LastName).NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .MaximumLength(80).WithMessage("{PropertyName} should not excede {MaxLenght} characteres");

            RuleFor(p => p.Birthday).NotEmpty().WithMessage("Birthday can not be empty");

            RuleFor(p => p.Telephone)
                .NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .MaximumLength(9).WithMessage("{PropertyName} should not excede {MaxLenght} characteres");

            RuleFor(p => p.Email).NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .EmailAddress().WithMessage("\"{PropertyName} must be a Email valid")
                .MaximumLength(100).WithMessage("{PropertyName} should not excede {MaxLenght} characteres");

            RuleFor(p => p.Address).NotEmpty().WithMessage("{PropertyName} Can not be empty")
                .MaximumLength(120).WithMessage("{PropertyName} should not excede {MaxLenght} characteres");

        }
    }
}
