namespace FIAP.Contacts.Create.Domain.ContactAggregate;

public class Address : EntityBase
{
    public Address(
        string street, 
        string number, 
        string city, 
        string district, 
        string state, 
        string zipcode,
        string? complement = null)
    {
        Street = street;
        Number = number;
        City = city;
        District = district;
        State = state;
        Zipcode = zipcode;
        Complement = complement;
    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string? Complement { get; private set; }
    public string City { get; private set; }
    public string District { get; private set; }
    public string State { get; private set; }
    public string Zipcode { get; private set; }
    public Contact Contact { get; private set; }
    public Guid ContactId { get; private set; }
}
