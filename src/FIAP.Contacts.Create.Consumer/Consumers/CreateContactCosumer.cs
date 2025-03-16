using AutoMapper;
using FIAP.Contacts.Create.Application.Handlers.Commands.AddContact;
using FIAP.Contacts.Create.Consumer.Dtos;
using FIAP.Contacts.Create.Consumer.Shared;
using MassTransit;
using MediatR;

namespace FIAP.Contacts.Create.Consumer.Consumers;

public class CreateContactConsumer : IConsumer<ContactDto>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateContactConsumer(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ContactDto> context)
    {
        var request = _mapper.Map<AddContactRequest>(context.Message);

        var response = await _mediator.Send(request, context.CancellationToken);

        if (response.IsError)
            throw new RetryException(string.Join(',', response.Errors.Select(x => x.Description)));
    }
}
