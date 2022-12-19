using AutoMapper;
using Bloggr.Application.Models.User;
using Bloggr.Application.Users.Queries.GetUsers;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<User, UsersQueryDto>();
        }
    }
}
