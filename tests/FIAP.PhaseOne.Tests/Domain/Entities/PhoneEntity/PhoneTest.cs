using FIAP.Contacts.Create.Domain.ContactAggregate;

namespace FIAP.Contacts.Create.Tests.Domain.Entities.PhoneEntity;

public class PhoneTest : DomainTest
{
    [Fact]
    public void CreatePhone_WithValidData_Succeeded()
    {
        var ddd = new Random().Next(1, 99);
        var phoneNumber = _faker.Phone.PhoneNumber();

        var phone = new Phone(ddd, phoneNumber);

        Assert.Equal(ddd, phone.DDD);
        Assert.Equal(phoneNumber, phone.Number);
    }
}