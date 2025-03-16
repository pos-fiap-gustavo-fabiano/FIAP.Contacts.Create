using FIAP.Contacts.Create.Application.Dto.Validators;
using FluentValidation;

namespace FIAP.Contacts.Create.Application.Handlers.Commands.AddContact.Validator;

public class AddContactValidator : AbstractValidator<AddContactRequest>
{
    public AddContactValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
            .MaximumLength(150).WithMessage(DefaultFailures.MaxLength);

        RuleFor(x => x.Name)
           .Matches("^[A-Za-záéíóúàèìòùâêîôûãõçñ]+(?: [A-Za-záéíóúàèìòùâêîôûãõçñ]+)+$")
           .WithMessage(DefaultFailures.InvalidName);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
            .MaximumLength(100).WithMessage(DefaultFailures.MaxLength)
            .EmailAddress().WithMessage(DefaultFailures.InvalidEmail);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(DefaultFailures.NotEmpty);

        RuleFor(x => x.Phone)
           .NotEmpty().WithMessage(DefaultFailures.NotEmpty);

        RuleFor(x => x.Address).SetValidator(new AddressDtoValidator());

        RuleFor(x => x.Phone).SetValidator(new PhoneDtoValidator());
    }
}
