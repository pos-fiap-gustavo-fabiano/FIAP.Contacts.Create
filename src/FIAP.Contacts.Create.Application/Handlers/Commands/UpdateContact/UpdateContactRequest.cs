using FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact.Dto;

namespace FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact;

public class UpdateContactRequest : IRequest<ErrorOr<Updated>>
{
    public Guid Id { get; set; }
    public required ContactForUpdateDto Contact { get; set; }
}
