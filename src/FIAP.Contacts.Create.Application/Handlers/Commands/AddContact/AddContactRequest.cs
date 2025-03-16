using FIAP.Contacts.Create.Application.Dto;

namespace FIAP.Contacts.Create.Application.Handlers.Commands.AddContact;

public class AddContactRequest : ContactDto, IRequest<ErrorOr<AddContactResponse>>
{
    
}
