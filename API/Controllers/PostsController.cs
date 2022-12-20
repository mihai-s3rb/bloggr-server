using AutoMapper;
using Bloggr.Application.Models;
using Bloggr.Application.Posts.Commands.CreatePost;
using Bloggr.Application.Posts.Commands.RemovePost;
using Bloggr.Application.Posts.Commands.UpdatePost;
using Bloggr.Application.Posts.Queries.GetById;
using Bloggr.Application.Posts.Queries.GetPage;
using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Domain.Models;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Bloggr.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePostDto> _validator;

        public PostsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        //GET post by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PostQueryDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetPostByIdQuery(id));
            return result;
        }

        //GET all POSTS
        [HttpGet(Name = "GetAllPosts")]
        public async Task<ActionResult<IEnumerable<PostsQueryDto>>> Get([FromQuery] string? input, [FromQuery] string[]? interests, [FromQuery] string? orderBy)
        {
            var posts = await _mediator.Send(new GetPostsQuery(input, interests, orderBy));
            return Ok(posts);
        }
        //GET posts by page
        [HttpGet("GetPage")]
        public async Task<ActionResult<PagedResultDto<PostsQueryDto>>> GetPage([FromQuery] int pageNumber = 1)
        {
            var pageDto = new PageModel
            {
               PageSize = 10,
               PageNumber = pageNumber
    };
            var pagedResult = await _mediator.Send(new GetPostsPageQuery(pageDto));
            return pagedResult;
        }
        //Create POST
        [HttpPost(Name = "AddPost")]
        public async Task<ActionResult<PostQueryDto>> Create([FromBody]CreatePostDto post)
        {
            //_validator.ValidateAndThrow(post);
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            return Ok(await _mediator.Send(new CreatePostCommand(post, post.Interests)));
        }

        //DELETE POST
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostQueryDto>> Delete(int id)
        {
            var result = await _mediator.Send(new RemovePostByIdCommand(id));
            return Ok(result);
        }

        [HttpPut(Name = "UpdatePost")]
        public async Task<ActionResult<PostQueryDto>> Update([FromBody]UpdatePostDto post)
        {
            //get the post with post.id
            //var postId = post.Id;

            ////var newPost = _mediator.Send(new Get)
            //var postFromDb = await _mediator.Send(new GetPostByIdQuery(postId));
            ////map the props
            //var mappedPost = _mapper.Map<UpdatePostDTO, PostQueryDto>(post, postFromDb);
            //actually update
            //var result = await _mediator.Send(new UpdatePostCommand(mappedPost));
            return Ok();
        }
    }
}
