using AutoMapper;
using Bloggr.Application.Messages.Queries;
using Bloggr.Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDto>();
        }
    }
}
