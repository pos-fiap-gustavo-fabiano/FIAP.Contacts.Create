using Bogus;
using FIAP.Contacts.Create.Application.Behaviors;
using FIAP.Contacts.Create.Application.Mapping;
using FIAP.Contacts.Create.Application.Shared;
using FIAP.Contacts.Create.Domain.ContactAggregate;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;

namespace FIAP.Contacts.Create.Tests.Application;

public abstract class IntegrationTest
{
    protected readonly Faker _faker = new("pt_BR");
    protected readonly CancellationToken _ct = new();
    protected readonly ServiceCollection _services = new();
    protected ISender _mediator;

    protected readonly Mock<IContactRepository> _contactRepositoryMock;


    public IntegrationTest()
    {
        _services.AddMediatR((x) => x.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(ApplicationServiceRegistration))!));

        _contactRepositoryMock = new Mock<IContactRepository>();
        _services.AddScoped(x => _contactRepositoryMock.Object);

        _services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ApplicationServiceRegistration)));

        _services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        _services.AddAutoMapper(typeof(MappingProfile));

        var provider = _services.BuildServiceProvider();

        _mediator = provider.GetRequiredService<ISender>();
    }

    public void Rebuild()
    {
        var provider = _services.BuildServiceProvider();

        _mediator = provider.GetRequiredService<ISender>();
    }
}