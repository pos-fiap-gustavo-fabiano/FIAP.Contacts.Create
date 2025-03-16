using AutoMapper;
using FIAP.Contacts.Create.Application.Dto;
using FIAP.Contacts.Create.Domain.ContactAggregate;

namespace FIAP.Contacts.Create.Application.Handlers.Queries.GetAllContacts;

public class GetAllContactsHandler(
    IContactRepository contactRepository,
    IMapper mapper) : IRequestHandler<GetAllContactsRequestDto, GetAllContactsResponseDto?>
{
    public async Task<GetAllContactsResponseDto?> Handle(
        GetAllContactsRequestDto request,
        CancellationToken ct)
    {
        var (contactsPaged, total) = await contactRepository.GetAll(request.Page, request.Limit, ct, request.DDD);

        var contacts = contactsPaged.Select(mapper.Map<ContactWithIdDto>);

        var response = new PaginationDto<ContactWithIdDto>(contacts, total, request.Page, request.Limit);

        return new GetAllContactsResponseDto { Contacts = response };
    }
}
