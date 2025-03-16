using MassTransit;

namespace FIAP.Contacts.Create.Consumer.Dtos;

[MessageUrn("ContactQueueService.Dto:ContactDto")]

public class ContactDto
{
    public required string Name { get; set; }
    public required PhoneDto Phone { get; set; }
    public required string Email { get; set; }
    public required AddressDto Address { get; set; }
}
