using AutoMapper;
using Bloggr.Application.Likes.Commands.CreateLike;
using Bloggr.Application.Likes.Queries.GetPostLikes;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<CreateLikeDto, Like>();
            CreateMap<Like, LikeQueryDto>();
        }
    }
}
