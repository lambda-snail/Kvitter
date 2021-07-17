
using AutoMapper;
using Flow.Core.DomainModels;
using FlowUI.ViewModels;

namespace FlowUI.AutoMapperProfiles
{
    /** Maps Post objects to and from NewPostViewModel objects. */
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            //CreateMap<Post, NewPostViewModel>();
            CreateMap<NewPostViewModel, Post>();
        }
    }
}
