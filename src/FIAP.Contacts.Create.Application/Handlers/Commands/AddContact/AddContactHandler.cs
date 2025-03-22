using System.Diagnostics;
using AutoMapper;
using FIAP.Contacts.Create.Domain.ContactAggregate;

namespace FIAP.Contacts.Create.Application.Handlers.Commands.AddContact;

public class AddContactHandler(
    IContactRepository contactRepository, ActivitySource activitySource,
    IMapper mapper) : IRequestHandler<AddContactRequest, ErrorOr<AddContactResponse>>
{
    public async Task<ErrorOr<AddContactResponse>> Handle(
        AddContactRequest request,
        CancellationToken ct)
    {
        using var activity = activitySource.StartActivity(
            "AddContactHandler.Processing",
            ActivityKind.Internal);
        var contact = mapper.Map<Contact>(request);

        if (contact is null)
            return Error.Failure(description: "não foi possível criar contato");

        await contactRepository.Add(contact, ct);
        activity?.SetStatus(ActivityStatusCode.Ok);
        return new AddContactResponse() { Id = contact.Id };
    }
}
