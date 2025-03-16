using FIAP.Contacts.Create.Domain.ContactAggregate;
using FIAP.Contacts.Create.Tests.Domain.Mock;

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

    [Fact]
    public void UpdatePhone_WithValidData_Succeeded()
    {
        var ddd = new Random().Next(1, 99);
        var phoneNumber = _faker.Phone.PhoneNumber();

        var phone = PhoneMock.Create();

        phone.Update(ddd, phoneNumber);

        Assert.Equal(ddd, phone.DDD);
        Assert.Equal(phoneNumber, phone.Number);
    }
}