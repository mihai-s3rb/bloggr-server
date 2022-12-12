using AutoMapper;
using Bloggr.Application.Models.Post;
using Bloggr.Domain.Interfaces;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<AddPostDTO, Post>();
            CreateMap<UpdatePostDTO, Post>();
        }
    }
}
