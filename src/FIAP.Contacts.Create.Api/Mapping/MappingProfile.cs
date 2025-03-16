using AutoMapper;
using FIAP.Contacts.Create.Api.Dto;
using FIAP.Contacts.Create.Application.Handlers.Commands.AddContact;

namespace FIAP.Contacts.Create.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ContactDto, AddContactRequest>().ReverseMap();
        CreateMap<ContactDto, Application.Dto.ContactDto>().ReverseMap();
        CreateMap<ContactWithIdDto, Application.Dto.ContactDto>().ReverseMap();
        CreateMap<ContactWithIdDto, Application.Dto.ContactWithIdDto>().ReverseMap();
        CreateMap<ContactDto, Application.Handlers.Commands.UpdateContact.Dto.ContactForUpdateDto>().ReverseMap();
        CreateMap<PhoneDto, Application.Dto.PhoneDto>().ReverseMap();
        CreateMap<AddressDto, Application.Dto.AddressDto>().ReverseMap();
    }
}
