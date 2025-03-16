using FIAP.Contacts.Create.Application.Dto;

namespace FIAP.Contacts.Create.Application.Handlers.Queries.GetAllContacts;

public class GetAllContactsResponseDto
{
    public required PaginationDto<ContactWithIdDto> Contacts { get; set; }
}
