using Application.Models;
using Application.Posts.Commands.CreatePost;
using AutoMapper;
using Bloggr.Application.Posts.Queries.GetPosts;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PostsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllPosts")]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            var posts = await _mediator.Send(new GetPostsQuery());
            return Ok(posts);
        }

        [HttpPost(Name = "AddPost")]
        public async Task<ActionResult<Post>> Create([FromBody]AddPostDTO post)
        {
            Post mappedPost = _mapper.Map<Post>(post);
            return Ok(await _mediator.Send(new CreatePostCommand(mappedPost)));
        }
    }
}
