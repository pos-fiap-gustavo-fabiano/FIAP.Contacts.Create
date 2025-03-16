using FIAP.Contacts.Create.Domain.ContactAggregate;
using FIAP.Contacts.Create.Tests.Domain.Mock;

namespace FIAP.Contacts.Create.Tests.Domain.Entities.ContactEntity;

public class ContactTest : DomainTest
{
    [Fact]
    public void CreateContact_WithValidData_CreatedWithSuccess()
    {
        var name = _faker.Name.FullName();
        var phone = PhoneMock.Create();
        var email = _faker.Internet.Email();
        var address = AddressMock.Create();

        var contact = new Contact(name, phone, email, address);

        Assert.Equal(name, contact.Name);
        Assert.Equal(email, contact.Email);
        Assert.Equal(phone.DDD, contact.Phone.DDD);
        Assert.Equal(phone.Number, contact.Phone.Number);
        Assert.Equal(address.Street, contact.Address.Street);
        Assert.Equal(address.Number, contact.Address.Number);
        Assert.Equal(address.City, contact.Address.City);
        Assert.Equal(address.District, contact.Address.District);
        Assert.Equal(address.State, contact.Address.State);
        Assert.Equal(address.Zipcode, contact.Address.Zipcode);
        Assert.Equal(address.Complement, contact.Address.Complement);
    }

    [Fact]
    public void UpdateContactNameAndEmail_WithValidData_UpdatedWithSuccess()
    {
        var name = _faker.Name.FullName();
        var email = _faker.Internet.Email();

        var contact = ContactMock.Create();

        contact.Update(name, email);

        Assert.Equal(name, contact.Name);
        Assert.Equal(email, contact.Email);
    }

    [Fact]
    public void UpdateContactPhone_WithValidData_UpdatedWithSuccess()
    {
        var ddd = new Random().Next(1, 99);
        var phoneNumber = _faker.Phone.PhoneNumber();

        var contact = ContactMock.Create();

        contact.UpdatePhone(ddd, phoneNumber);

        Assert.Equal(ddd, contact.Phone.DDD);
        Assert.Equal(phoneNumber, contact.Phone.Number);
    }

    [Fact]
    public void UpdateContactAddress_WithValidData_UpdatedWithSuccess()
    {
        var address = AddressMock.Create();

        var contact = ContactMock.Create();

        contact.UpdateAddress(
            address.Street,
            address.Number,
            address.City,
            address.District,
            address.State,
            address.Zipcode,
            address.Complement);

        Assert.Equal(address.Street, contact.Address.Street);
        Assert.Equal(address.Number, contact.Address.Number);
        Assert.Equal(address.City, contact.Address.City);
        Assert.Equal(address.District, contact.Address.District);
        Assert.Equal(address.State, contact.Address.State);
        Assert.Equal(address.Zipcode, contact.Address.Zipcode);
        Assert.Equal(address.Complement, contact.Address.Complement);
    }
}