using AutoMapper;
using HouseRules.Models;
using HouseRules.Models.DTOs;

namespace Mapping.MappingProfile;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserProfile, UserProfileDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.IdentityUser.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.IdentityUser.Email));

        CreateMap<Chore, ChoreDTO>();

          CreateMap<CreateChoreDTO, Chore>();
        CreateMap<Chore, ChoreDTO>();

          CreateMap<UpdateChoreDTO, Chore>();


    }
}
