using AutoMapper;
using Bloggr.Application.Models.UserModel;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
        }
    }
}
