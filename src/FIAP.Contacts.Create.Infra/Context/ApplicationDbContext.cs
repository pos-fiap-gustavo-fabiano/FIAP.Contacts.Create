using Microsoft.EntityFrameworkCore;
using FIAP.Contacts.Create.Domain.ContactAggregate;

namespace FIAP.Contacts.Create.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
              
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Address)
            .WithOne(a => a.Contact)
                .HasForeignKey<Address>(a => a.ContactId);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Phone)
            .WithOne(p => p.Contact)
                .HasForeignKey<Phone>(p => p.ContactId);

            modelBuilder.Entity<Phone>().HasKey(x => x.Id);
            modelBuilder.Entity<Address>().HasKey(x => x.Id);
        }
    }
}
