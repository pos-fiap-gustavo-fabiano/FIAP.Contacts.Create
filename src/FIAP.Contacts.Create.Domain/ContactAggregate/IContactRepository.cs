namespace FIAP.Contacts.Create.Domain.ContactAggregate;

public interface IContactRepository
{
    Task Add(Contact contact, CancellationToken ct);
    Task<Contact?> GetById(Guid id, CancellationToken ct);
    Task Update(Contact contact, CancellationToken ct);
    Task Remove(Guid id, CancellationToken ct);
    Task<(IEnumerable<Contact> Items, int Total)> GetAll(int page, int limit, CancellationToken ct, int? ddd = null);
    Task SaveChanges(CancellationToken ct);
}
