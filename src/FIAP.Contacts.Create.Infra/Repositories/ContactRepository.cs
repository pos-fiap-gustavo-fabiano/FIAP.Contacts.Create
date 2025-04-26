using System.Diagnostics;
using FIAP.Contacts.Create.Domain.ContactAggregate;
using FIAP.Contacts.Create.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Contacts.Create.Infra.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ActivitySource _activitySource;


        public ContactRepository(ApplicationDbContext context, ActivitySource activitySource)
        {
            _context = context;
            _activitySource = activitySource;
        }

        public async Task Add(Contact contact, CancellationToken ct)
        {
            using var activity = _activitySource.StartActivity(
           "ContactRepository.Add",
           ActivityKind.Consumer
           );
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync(ct);
            activity?.SetStatus(ActivityStatusCode.Ok);
        }

        public async Task<Contact?> GetById(Guid id, CancellationToken ct) =>
            await _context.Contacts.Include(c => c.Phone)
                                    .Include(c => c.Address)
                                    .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task SaveChanges(CancellationToken ct) => await _context.SaveChangesAsync(ct);
    }

}
