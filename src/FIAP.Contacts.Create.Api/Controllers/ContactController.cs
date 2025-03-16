using AutoMapper;
using ErrorOr;
using FIAP.Contacts.Create.Api.Controllers.Shared;
using FIAP.Contacts.Create.Application.Dto;
using FIAP.Contacts.Create.Application.Handlers.Commands.AddContact;
using FIAP.Contacts.Create.Application.Handlers.Commands.DeleteContact;
using FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact;
using FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact.Dto;
using FIAP.Contacts.Create.Application.Handlers.Queries.GetAllContacts;
using FIAP.Contacts.Create.Application.Handlers.Queries.GetContactById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ContactDto = FIAP.Contacts.Create.Api.Dto.ContactDto;
using ContactWithIdDto = FIAP.Contacts.Create.Api.Dto.ContactWithIdDto;

namespace FIAP.Contacts.Create.Api.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    [ProducesResponseType<Error[]>((int)HttpStatusCode.BadRequest)]
    public class ContactController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ContactController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType<AddContactResponse>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateContact(ContactDto contactDto, CancellationToken ct)
        {
            var request = _mapper.Map<AddContactRequest>(contactDto);

            var response = await _mediator.Send(request, ct);

            if (response.IsError)
                return BadRequest(response.Errors);
            
            return Ok(response.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<ContactDto>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetContactById(Guid id, CancellationToken ct)
        {
            var response = await _mediator.Send(new GetContactByIdRequestDto { Id = id }, ct);

            if (response is null) return NotFound();

            var contact = _mapper.Map<ContactDto>(response.Contact);

            return Ok(contact);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateContact(
            Guid id,
            [FromBody] ContactDto contactDto,
            CancellationToken ct)
        {
            var contact = _mapper.Map<ContactForUpdateDto>(contactDto);

            var response = await _mediator.Send(
                new UpdateContactRequest
                {
                    Id = id,
                    Contact = contact
                }, ct);

            if (response.IsError)
                return BadRequest(response.Errors);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteContact(Guid id, CancellationToken ct)
        {
            var response = await _mediator.Send(new DeleteContactRequest { Id = id}, ct);

            if (response.IsError)
                return BadRequest(response.Errors);

            return NoContent();
        }


        [HttpGet]
        [ProducesResponseType<PaginationDto<ContactWithIdDto>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllContacts(
            CancellationToken ct, int page = 1, int limit = 10, int? ddd = null)
        {
            var response = await _mediator.Send(
                new GetAllContactsRequestDto { Page = page, Limit = limit, DDD = ddd }, ct);

            return Ok(response.Contacts);
        }
    }
}
