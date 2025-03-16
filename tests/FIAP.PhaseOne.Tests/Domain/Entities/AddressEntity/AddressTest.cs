using FIAP.Contacts.Create.Tests.Domain.Mock;

namespace FIAP.Contacts.Create.Tests.Domain.Entities.AddressEntity;

public class AddressTest : DomainTest
{
    [Fact]
    public void CreateAddress_WithValidData_Succeeded()
    {
        var streetName = _faker.Address.StreetName();
        var number = _faker.Address.BuildingNumber();
        var city = _faker.Address.City();
        var district = _faker.Address.County();
        var state = _faker.Address.StateAbbr();
        var zipCode = _faker.Address.ZipCode("########");
        var complement = _faker.Address.SecondaryAddress();

        var address = new PhaseOne.Domain.ContactAggregate.Address(
            streetName,
            number,
            city,
            district,
            state,
            zipCode,
            complement);

        Assert.Equal(streetName, address.Street);
        Assert.Equal(number, address.Number);
        Assert.Equal(city, address.City);
        Assert.Equal(district, address.District);
        Assert.Equal(state, address.State);
        Assert.Equal(zipCode, address.Zipcode);
        Assert.Equal(complement, address.Complement);
    }

    [Fact]
    public void UpdateAddress_WithValidData_Succeeded()
    {
        var streetName = _faker.Address.StreetName();
        var number = _faker.Address.BuildingNumber();
        var city = _faker.Address.City();
        var district = _faker.Address.County();
        var state = _faker.Address.StateAbbr();
        var zipCode = _faker.Address.ZipCode("########");
        var complement = _faker.Address.SecondaryAddress();

        var address = AddressMock.Create();

        address.Update(streetName, number, city, district, state, zipCode, complement);

        Assert.Equal(streetName, address.Street);
        Assert.Equal(number, address.Number);
        Assert.Equal(city, address.City);
        Assert.Equal(district, address.District);
        Assert.Equal(state, address.State);
        Assert.Equal(zipCode, address.Zipcode);
        Assert.Equal(complement, address.Complement);
    }
}