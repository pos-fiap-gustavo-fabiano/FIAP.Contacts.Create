using FIAP.Contacts.Create.Domain.ContactAggregate;
using FIAP.Contacts.Create.Infra.Context;
using FIAP.Contacts.Create.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Contacts.Create.Infra
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));

            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }

        public static IServiceProvider UpdateMigrate(this IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();

            return serviceProvider;
        }
    }
}
