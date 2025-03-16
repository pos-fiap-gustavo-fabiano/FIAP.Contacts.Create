using AutoMapper;
using FIAP.Contacts.Create.Application.Dto;
using FIAP.Contacts.Create.Application.Handlers.Commands.AddContact;
using FIAP.Contacts.Create.Domain.ContactAggregate;

namespace FIAP.Contacts.Create.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddContactRequest, Contact>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, ContactWithIdDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();

        }
    }
}
