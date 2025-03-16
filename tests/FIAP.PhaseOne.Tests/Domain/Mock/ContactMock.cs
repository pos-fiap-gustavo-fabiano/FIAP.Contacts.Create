using Bogus;
using FIAP.Contacts.Create.Domain.ContactAggregate;

namespace FIAP.Contacts.Create.Tests.Domain.Mock;

public static class ContactMock
{
    private static readonly Faker _faker = new("pt_BR");

    public static Contact Create()
    {
        var name = _faker.Name.FullName();
        var phone = new Phone(new Random().Next(1, 99), _faker.Phone.PhoneNumber());
        var email = _faker.Internet.Email();
        var address = new Address(
            _faker.Address.StreetName(),
            _faker.Address.BuildingNumber(),
            _faker.Address.City(),
            _faker.Address.County(),
            _faker.Address.StateAbbr(),
            _faker.Address.ZipCode("########"),
            _faker.Address.SecondaryAddress());

        var contact = new Contact(name, phone, email, address);

        return contact;
    }
}