using FIAP.Contacts.Create.Application.Dto;

namespace FIAP.Contacts.Create.Application.Handlers.Queries.GetContactById;

public class GetContactByIdResponseDto
{
    public required ContactDto Contact { get; set; }
}
