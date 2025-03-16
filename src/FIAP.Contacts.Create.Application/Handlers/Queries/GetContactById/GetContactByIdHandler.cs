using AutoMapper;
using FIAP.Contacts.Create.Application.Dto;
using FIAP.Contacts.Create.Domain.ContactAggregate;

namespace FIAP.Contacts.Create.Application.Handlers.Queries.GetContactById;

public class GetContactByIdHandler(
    IContactRepository contactRepository,
    IMapper mapper) : IRequestHandler<GetContactByIdRequestDto, GetContactByIdResponseDto?>
{
    public async Task<GetContactByIdResponseDto?> Handle(
        GetContactByIdRequestDto request,
        CancellationToken ct)
    {
        var contact = await contactRepository.GetById(request.Id, ct);

        if (contact is null) return null;

        var contactMapped = mapper.Map<ContactDto>(contact);

        return new GetContactByIdResponseDto { Contact = contactMapped };
    }
}
