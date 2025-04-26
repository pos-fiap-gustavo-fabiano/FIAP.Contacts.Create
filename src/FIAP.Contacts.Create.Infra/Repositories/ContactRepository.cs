using FIAP.Contacts.Create.Domain.ContactAggregate;
using FIAP.Contacts.Create.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Contacts.Create.Infra.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Contact contact, CancellationToken ct)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Contact?> GetById(Guid id, CancellationToken ct) =>
            await _context.Contacts.Include(c => c.Phone)
                                    .Include(c => c.Address)
                                    .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task SaveChanges(CancellationToken ct) => await _context.SaveChangesAsync(ct);
    }

}
