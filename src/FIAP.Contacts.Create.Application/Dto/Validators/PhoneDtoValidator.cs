using FluentValidation;

namespace FIAP.Contacts.Create.Application.Dto.Validators
{
    internal class PhoneDtoValidator: AbstractValidator<PhoneDto>
    {
        public PhoneDtoValidator()
        {
            RuleFor(x => x.DDD)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
                .GreaterThan(0).WithMessage(DefaultFailures.InvalidDDD)
                .LessThan(99).WithMessage(DefaultFailures.InvalidDDD);

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty)
                .MaximumLength(9).WithMessage(DefaultFailures.MaxLength)
                .Matches("^[0-9]+$").WithMessage(DefaultFailures.OnlyNumbers);
        }
    }
}
