using AutoMapper;
using Bloggr.Application.Comments.Commands.CreateComment;
using Bloggr.Application.Comments.Queries.GetPostComments;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Comment, CommentQueryDto>();
        }
    }
}
