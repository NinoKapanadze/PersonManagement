using AutoMapper;
using PersonManagement.Application.DTOs;
using PersonManagement.Domain;

namespace PersonManagement.Application.MappingProfiles
{
    //public class PersonMappingProfile : Profile
    //{
    //    public PersonMappingProfile()
    //    {
    //        CreateMap<Person, PersonDTO>();
    //        CreateMap<PhoneNumber, PhoneNumberDto>();
    //        CreateMap<RelatedPerson, RelatedPersonDTO>()
    //        .ForMember(dest => dest.RelatedPersonId, opt => opt.MapFrom(src => src.RelatedTo.Id))
    //        .ForMember(dest => dest.RelatedPerson, opt => opt.MapFrom(src => src.RelatedTo))
    //        .ForMember(dest => dest.RelationshipType, opt => opt.MapFrom(src => src.RelationshipType));
    //    }
    //}
}
//TODO: თუ არ გამოვიყენებ წავშალო