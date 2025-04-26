using FIAP.Contacts.Create.Consumer.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using FIAP.Contacts.Create.Application.Shared;
using FIAP.Contacts.Create.Infra;
using FIAP.Contacts.Create.Consumer.DI;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Resources;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using MassTransit.Logging;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddEndpointsApiExplorer();
        services.AddInfraServices(builder.Configuration);
        services.AddApplicationService();
        services.AddConsumers();
        //services.AddMetricsConfig();

        var config = builder.Configuration;

        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateContactConsumer>((x) =>
            {
                x.UseMessageRetry(r =>
                {
                    r.Interval(3, TimeSpan.FromMilliseconds(300));
                });

                x.ConcurrentMessageLimit = 1;
            });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(config.GetValue<string>("RabbitMq:Host"));

                cfg.ReceiveEndpoint(config.GetValue("RabbitMq:QueueName", "contact.create"), e =>
                {
                    e.SetQueueArgument("x-dead-letter-exchange", "");
                    e.SetQueueArgument("x-dead-letter-routing-key", "contact.dlq");

                    e.ConfigureConsumer<CreateContactConsumer>(context);
                });
            });
        });
        var serviceName = "fiap-contact-create";
        var serviceVersion = "1.0.0";

        // Create a single ActivitySource that can be used throughout the application
        var activitySource = new ActivitySource(serviceName, serviceVersion);
        services.AddSingleton(activitySource);

        var resourceBuilder = ResourceBuilder.CreateDefault()
          .AddService(serviceName: serviceName, serviceVersion: serviceVersion);

        services.AddOpenTelemetry()
               .ConfigureResource(resource => resource.AddService(
                   serviceName: serviceName,
                   serviceVersion: serviceVersion))
               .WithTracing(tracing => tracing
                   .AddSource(serviceName)
                   .AddSource(DiagnosticHeaders.DefaultListenerName)
                   .AddAspNetCoreInstrumentation()
                   .AddHttpClientInstrumentation()
                   .AddEntityFrameworkCoreInstrumentation(x => x.SetDbStatementForText = true) // Add if using EF Core
                   .AddOtlpExporter(options =>
                   {
                       options.Endpoint = new Uri("http://134.122.121.176:4317");
                       options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                   }));

        // Configure OpenTelemetry logging
        services.AddLogging(logging =>
        {
            logging.AddOpenTelemetry(options =>
            {
                options.SetResourceBuilder(resourceBuilder);
                options.IncludeFormattedMessage = true;
                options.IncludeScopes = true;

                options.AddOtlpExporter(exporterOptions =>
                {
                    exporterOptions.Endpoint = new Uri("http://134.122.121.176:4317");
                    exporterOptions.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                });
            });
        });
    })
    .Build();

await host.RunAsync();
