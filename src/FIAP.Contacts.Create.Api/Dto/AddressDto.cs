namespace FIAP.Contacts.Create.Api.Dto;

public class AddressDto
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string? Complement { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string State { get; set; }
    public string Zipcode { get; set; }
}
