using AutoMapper;
using FIAP.Contacts.Create.Application.Handlers.Commands.AddContact;
using FIAP.Contacts.Create.Consumer.Dtos;

namespace FIAP.Contacts.Create.Consumer.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ContactDto, AddContactRequest>().ReverseMap();
        CreateMap<AddressDto, Application.Dto.AddressDto>().ReverseMap();
        CreateMap<PhoneDto, Application.Dto.PhoneDto>().ReverseMap();
    }
}
    