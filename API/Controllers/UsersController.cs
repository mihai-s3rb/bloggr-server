using AutoMapper;
using Bloggr.Application.Bookmarks.Commands.CreateBookmark;
using Bloggr.Application.Bookmarks.Commands.RemoveBookmark;
using Bloggr.Application.Interests.Commands.CreateInterest;
using Bloggr.Application.Interests.Commands.RemoveInterest;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Interests.Queries.GetPostInterests;
using Bloggr.Application.Models;
using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Application.Users;
using Bloggr.Application.Users.Commands.CreateUser;
using Bloggr.Application.Users.Commands.RemoveUser;
using Bloggr.Application.Users.Commands.UpdateUser;
using Bloggr.Application.Users.Queries.GetUserById;
using Bloggr.Application.Users.Queries.GetUserByUsername;
using Bloggr.Application.Users.Queries.GetUsers;
using Bloggr.Application.Users.Queries.LoginUser;
using Bloggr.Domain.Models;
using Bloggr.WebUI.CustomModelBinder;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersQueryDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }
        [HttpGet("username/${username}")]
        public async Task<ActionResult<UsersQueryDto>> GetByUsername(string username)
        {
            return Ok(await _mediator.Send(new GetUserByUsernameQuery(username)));
        }

        [HttpGet(Name = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UsersQueryDto>>> Get()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<ActionResult<CredentialsModel>> Register([FromBody] CreateUserDto user)
        {
            return Accepted(await _mediator.Send(new CreateUserCommand(user, user.Interests)));
        }

        [HttpPost("login")]
        public async Task<ActionResult<CredentialsModel>> Login([FromBody] LoginUserDto user)
        {
            return Accepted(await _mediator.Send(new LoginUser(user)));
        }

        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<UsersQueryDto>> Delete(int id)
        {
            var result = await _mediator.Send(new RemoveUserByIdCommand(id));
            return Ok(result);
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<UserDto>> Update( [FromForm] UpdateUserDto user)
        {
            return Ok(await _mediator.Send(new UpdateUserCommand(user)));
        }

        //related
        //[HttpGet("{id}/createdInterests")]
        //public async Task<ActionResult<InterestQueryDto>> GetUserInterests(int id)
        //{
        //    return Ok();
        //    //return Ok(await _mediator.Send(new GetPostInterestsQuery(id)));
        //}

        [HttpPost("createdInterests")]
        [Authorize]
        public async Task<ActionResult<InterestQueryDto>> CreateUserInterest(CreateInterestDto interest)
        {
            return Ok(await _mediator.Send(new CreateInterestCommand(interest)));
        }

        [HttpPost("bookmarks")]
        [Authorize]
        public async Task<ActionResult<Post>> AddBookmark(int postId)
        {
            return Ok(await _mediator.Send(new CreateBookmarkCommand(postId)));
        }

        [HttpDelete("bookmarks")]
        [Authorize]
        public async Task<ActionResult<Post>> DeleteBookmark(int postId)
        {
            return Ok(await _mediator.Send(new RemoveBookmarkCommand(postId)));
        }
    }
}
