namespace FIAP.Contacts.Create.Api.Dto;

public class ContactDto
{
    public required string Name { get; set; }
    public required PhoneDto Phone { get; set; }
    public required string Email { get; set; }
    public required AddressDto Address { get; set; }
}
