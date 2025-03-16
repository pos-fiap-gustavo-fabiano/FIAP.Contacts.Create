using FluentValidation;

namespace FIAP.Contacts.Create.Application.Handlers.Commands.DeleteContact.Validator
{
    internal class DeleteContactValidator : AbstractValidator<DeleteContactRequest>
    {
        public DeleteContactValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(DefaultFailures.NotEmpty);
        }
    }
}
