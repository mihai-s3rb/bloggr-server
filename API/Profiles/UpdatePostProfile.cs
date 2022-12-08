using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class UpdatePostProfile : Profile
    {
        public UpdatePostProfile()
        {
            CreateMap<UpdatePostDTO, Post>();
        }
    }
}
