namespace FIAP.Contacts.Create.Domain.ContactAggregate;

public class Contact : EntityBase
{
    public Contact() {}

    public Contact(
        string name,
        Phone phone,
        string email,
        Address address)
    {
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
    }

    public string Name { get; private set; }
    public Phone Phone { get; private set; }
    public string Email { get; private set; }
    public Address Address { get; private set; }

    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public void UpdatePhone(int ddd, string number) => Phone.Update(ddd, number);

    public void UpdateAddress(
        string street,
        string number,
        string city,
        string district,
        string state,
        string zipcode,
        string? complement = null) => 
            Address.Update(street, number, city, district, state, zipcode, complement);


}
