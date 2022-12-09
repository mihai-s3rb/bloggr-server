using AutoMapper;
using Bloggr.Application.Models.Like;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<AddLikeDTO, Like>();
        }
    }
}
