using FIAP.Contacts.Create.Domain.ContactAggregate;

namespace FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact;

public class UpdateContactHandler(IContactRepository contactRepository) 
    : IRequestHandler<UpdateContactRequest, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(
        UpdateContactRequest request,
        CancellationToken ct)
    {
        var contact = await contactRepository.GetById(request.Id, ct);

        if (contact is null) return Error.NotFound();

        contact.Update(request.Contact.Name, request.Contact.Email);

        contact.UpdatePhone(request.Contact.Phone.DDD, request.Contact.Phone.Number);

        var newAddress = request.Contact.Address;
        contact.UpdateAddress(
            newAddress.Street, 
            newAddress.Number, 
            newAddress.City, 
            newAddress.District, 
            newAddress.State, 
            newAddress.Zipcode, 
            newAddress.Complement);

        await contactRepository.SaveChanges(ct);

        return Result.Updated;
    }
}
