using Application.Models;
using Application.Posts.Commands.CreatePost;
using AutoMapper;
using Bloggr.Application.Posts.Commands.RemovePost;
using Bloggr.Application.Posts.Commands.UpdatePost;
using Bloggr.Application.Posts.Queries.GetById;
using Bloggr.Application.Posts.Queries.GetPosts;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggr.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IValidator<AddPostDTO> _validator;

        public PostsController(IMediator mediator, IMapper mapper, IValidator<AddPostDTO> validator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetPostByIdQuery(id));
            return result;
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
            var result = await _validator.ValidateAsync(post);
            Post mappedPost = _mapper.Map<Post>(post);
            return Ok(await _mediator.Send(new CreatePostCommand(mappedPost)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            var result = await _mediator.Send(new RemovePostByIdCommand(id));
            return Ok(result);
        }

        [HttpPut(Name = "UpdatePost")]
        public async Task<ActionResult<Post>> Update([FromBody]UpdatePostDTO post)
        {
            //get the post with post.id
            var postId = post.Id;

            //var newPost = _mediator.Send(new Get)
            var postFromDb = await _mediator.Send(new GetPostByIdQuery(postId));
            //map the props
            var mappedPost = _mapper.Map<UpdatePostDTO, Post>(post, postFromDb);
            //actually update
            var result = await _mediator.Send(new UpdatePostCommand(mappedPost));
            return Ok(result);
        }
    }
}
