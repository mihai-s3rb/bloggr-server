using AutoMapper;
using Bloggr.Application.Models.Interest;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class InterestProfile : Profile
    {
        public InterestProfile()
        {
            CreateMap<AddInterestDTO, Interest>();
        }
    }
}
