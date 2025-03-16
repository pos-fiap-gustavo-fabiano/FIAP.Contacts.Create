using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Contacts.Create.Infra.Context;

public class ApplicationDbContextDesignTime : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {

        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
        options.UseNpgsql();

        return new ApplicationDbContext(options.Options);
    }
}
