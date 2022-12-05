using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<AddPostDTO, Post>();
        }
    }
}
