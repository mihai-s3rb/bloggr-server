using AutoMapper;
using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Application.Users;
using Bloggr.Application.Users.Commands.CreateUser;
using Bloggr.Application.Users.Commands.UpdateUser;
using Bloggr.Application.Users.Queries.GetUsers;
using Domain.Entities;

namespace Bloggr.WebUI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<User, UsersQueryDto>()
                .ForMember(dest => dest.Interests, opt => opt.MapFrom(x => x.InterestUsers.Select(x => x.Interest)));
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Interests, opt => opt.MapFrom(x => x.InterestUsers.Select(x => x.Interest)));
        }
    }
}
