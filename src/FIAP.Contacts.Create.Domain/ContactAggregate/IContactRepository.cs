namespace FIAP.Contacts.Create.Domain.ContactAggregate;

public interface IContactRepository
{
    Task Add(Contact contact, CancellationToken ct);
    Task<Contact?> GetById(Guid id, CancellationToken ct);
    Task SaveChanges(CancellationToken ct);
}
