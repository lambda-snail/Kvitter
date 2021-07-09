
using AutoMapper;
using Flow.Core.DomainModels;
using FlowUI.ViewModels;

namespace FlowUI.AutoMapperProfiles
{
    /** Maps User objects to and from UserViewModel objects. */
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.Age.ToString()));

            CreateMap<UserViewModel, User>()
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => int.Parse(src.Age)));

        }
    }
}
