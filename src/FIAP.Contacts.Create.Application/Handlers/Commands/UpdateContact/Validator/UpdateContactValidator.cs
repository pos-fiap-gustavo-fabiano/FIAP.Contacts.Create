using FIAP.Contacts.Create.Application.Dto.Validators;
using FluentValidation;

namespace FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact.Validator;

public class UpdateContactValidator : AbstractValidator<UpdateContactRequest>
{
    public UpdateContactValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(DefaultFailures.NotEmpty);

        RuleFor(x => x.Contact)
            .NotEmpty().WithMessage(DefaultFailures.NotEmpty);

        When(x => x.Contact is not null, () =>
        {

            RuleFor(x => x.Contact.Name)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
                .MaximumLength(150).WithMessage(DefaultFailures.MaxLength);

            RuleFor(x => x.Contact.Name)
               .Matches("^[A-Za-záéíóúàèìòùâêîôûãõçñ]+(?: [A-Za-záéíóúàèìòùâêîôûãõçñ]+)+$")
               .WithMessage(DefaultFailures.InvalidName);

            RuleFor(x => x.Contact.Email)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
                .MaximumLength(100).WithMessage(DefaultFailures.MaxLength)
                .EmailAddress().WithMessage(DefaultFailures.InvalidEmail);

            RuleFor(x => x.Contact.Address)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty);

            RuleFor(x => x.Contact.Phone)
               .NotEmpty().WithMessage(DefaultFailures.NotEmpty);

            RuleFor(x => x.Contact.Address).SetValidator(new AddressDtoValidator());

            RuleFor(x => x.Contact.Phone).SetValidator(new PhoneDtoValidator());
        });
    }
}
