using AutoMapper;
using Bloggr.Application.Models.User;
using Bloggr.Application.Users.Commands.CreateUser;
using Bloggr.Application.Users.Commands.RemoveUser;
using Bloggr.Application.Users.Commands.UpdateUser;
using Bloggr.Application.Users.Queries.GetUserById;
using Bloggr.Application.Users.Queries.GetUsers;
using Domain.Entities;
using MediatR;
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
        public async Task<ActionResult<User?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return result;
        }

        [HttpGet(Name = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }

        [HttpPost(Name = "AddUser")]
        public async Task<ActionResult<User>> Create([FromBody] AddUserDTO user)
        {
            User mappedUser = _mapper.Map<User>(user);
            return Ok(await _mediator.Send(new CreateUserCommand(mappedUser)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var result = await _mediator.Send(new RemoveUserByIdCommand(id));
            return Ok(result);
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<ActionResult<Post>> Update([FromBody] UpdateUserDTO user)
        {
            //get the post with post.id
            var userId = user.Id;
            //var newPost = _mediator.Send(new Get)
            var userFromDb = await _mediator.Send(new GetUserByIdQuery(userId));
            //map the props
            var mappedUser = _mapper.Map<UpdateUserDTO, User>(user, userFromDb);
            //actually update
            var result = await _mediator.Send(new UpdateUserCommand(mappedUser));
            return Ok(result);
        }
    }
}
