using FIAP.Contacts.Create.Application.Behaviors;
using FIAP.Contacts.Create.Application.Mapping;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FIAP.Contacts.Create.Application.Shared
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {

            services.AddMediatR((x) => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
