using FIAP.Contacts.Create.Application.Dto;

namespace FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact.Dto;

public record ContactForUpdateDto(string Name, PhoneDto Phone, string Email, AddressDto Address);
