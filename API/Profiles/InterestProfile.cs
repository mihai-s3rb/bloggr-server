using AutoMapper;
using Bloggr.Application.Interests.Commands.CreateInterest;
using Bloggr.Application.Interests.Queries.GetInterests;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class InterestProfile : Profile
    {
        public InterestProfile()
        {
            CreateMap<CreateInterestDto, Interest>();
            CreateMap<Interest, InterestQueryDto>();
        }
    }
}
