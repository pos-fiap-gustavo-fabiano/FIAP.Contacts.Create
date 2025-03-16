using FIAP.Contacts.Create.Api.Mapping;
using Serilog;
using Serilog.Extensions.Logging;

namespace FIAP.Contacts.Create.Api;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {

        var loggerConfig = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", "FIAP.Contacts.Create.Api")
            .WriteTo.Console()
            .CreateLogger();

        services.AddSingleton<ILoggerFactory>(new SerilogLoggerFactory(loggerConfig));
        services.AddLogging();

        services.AddAutoMapper(typeof(MappingProfile));
        
        return services;
    }
}
