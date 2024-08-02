using AutoMapper;
using Models.DTOS;
using Models.Entities;


namespace Utilities;

public class MappingProfile : Profile
{
    public MappingProfile() 
    { 
        CreateMap<Speciality, SpecialityDto>()
            .ForMember(d => d.state, m => m.MapFrom(o => o.state == true ? 1 : 0)); // la DB me trae un Int y con este mapper se convierte en bool
    }
}
