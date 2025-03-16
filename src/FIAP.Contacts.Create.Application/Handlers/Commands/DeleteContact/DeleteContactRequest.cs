namespace FIAP.Contacts.Create.Application.Handlers.Commands.DeleteContact;

public class DeleteContactRequest : IRequest<ErrorOr<Deleted>>
{
    public Guid Id { get; set; }
}
