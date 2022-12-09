using AutoMapper;
using Bloggr.Application.Models.Comment;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<AddCommentDTO, Comment>();
        }
    }
}
