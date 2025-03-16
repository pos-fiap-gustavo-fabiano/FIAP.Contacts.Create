using FIAP.Contacts.Create.Consumer.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using FIAP.Contacts.Create.Application.Shared;
using FIAP.Contacts.Create.Infra;
using FIAP.Contacts.Create.Consumer.DI;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
     .ConfigureLogging(logging =>
     {
         logging.AddConsole();
     })
    .ConfigureServices((builder, services) =>
    {
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
    })
    .Build();

await host.RunAsync();
