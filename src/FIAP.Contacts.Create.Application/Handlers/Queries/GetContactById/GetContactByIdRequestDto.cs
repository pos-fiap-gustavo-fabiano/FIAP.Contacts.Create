namespace FIAP.Contacts.Create.Application.Handlers.Queries.GetContactById;

public class GetContactByIdRequestDto : IRequest<GetContactByIdResponseDto>
{
    public Guid Id { get; set; }
}
