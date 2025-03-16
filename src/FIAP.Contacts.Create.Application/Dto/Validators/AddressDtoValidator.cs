using FluentValidation;

namespace FIAP.Contacts.Create.Application.Dto.Validators
{
    internal class AddressDtoValidator: AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
                .MaximumLength(150).WithMessage(DefaultFailures.MaxLength);

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
                .MaximumLength(50).WithMessage(DefaultFailures.MaxLength);

            RuleFor(x => x.District)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
                .MaximumLength(100).WithMessage(DefaultFailures.MaxLength);

            RuleFor(x => x.City)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
                .MaximumLength(100).WithMessage(DefaultFailures.MaxLength);

            RuleFor(x => x.State)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
                .MaximumLength(2).WithMessage(DefaultFailures.MaxLength);

            RuleFor(x => x.Zipcode)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
                .MaximumLength(8).WithMessage(DefaultFailures.MaxLength);
        }
    }
}
