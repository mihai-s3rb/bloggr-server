using AutoMapper;
using Bloggr.Application.Comments.Commands.CreateComment;
using Bloggr.Application.Comments.Commands.RemoveComment;
using Bloggr.Application.Comments.Queries.GetPostComments;
using Bloggr.Application.Interests.Queries.GetPostInterests;
using Bloggr.Application.Likes.Commands.CreateLike;
using Bloggr.Application.Likes.Commands.RemoveLike;
using Bloggr.Application.Likes.Queries.GetPostLikes;
using Bloggr.Application.Models;
using Bloggr.Application.Posts.Commands.CreatePost;
using Bloggr.Application.Posts.Commands.RemovePost;
using Bloggr.Application.Posts.Commands.UpdatePost;
using Bloggr.Application.Posts.Queries.GetById;
using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Domain.Exceptions;
using Bloggr.Domain.Models;
using Bloggr.Infrastructure.Interfaces;
using Bloggr.Infrastructure.Models.Blobs;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Net;

namespace Bloggr.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAzureStorage _storage;

        public PostsController(IMediator mediator, IMapper mapper, IAzureStorage storage)
        {
            _mediator = mediator;
            _mapper = mapper;
            _storage = storage;
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
        public async Task<ActionResult<PagedResultDto<PostsQueryDto>>> Get([FromQuery] string? username, [FromQuery] string? input, [FromQuery] string[]? interests, [FromQuery] string? orderBy, bool isBookmarked = false, int pageNumber = 1)
        {
            //throw EntityNotFoundException.OfType<Post>();
            var pageDto = new PageModel
            {
                PageSize = 10,
                PageNumber = pageNumber
            };
            var posts = await _mediator.Send(new GetPostsQuery(pageDto, username, input, interests, orderBy, isBookmarked));
            return Ok(posts);
        }
        //Create POST
        [HttpPost(Name = "AddPost")]
        [Authorize]
        public async Task<ActionResult<PostQueryDto>> Create([FromBody]CreatePostDto post)
        {
            return Ok(await _mediator.Send(new CreatePostCommand(post, post.Interests)));
        }

        //DELETE POST
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<PostQueryDto>> Delete(int id)
        {
            var result = await _mediator.Send(new RemovePostByIdCommand(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<PostQueryDto>> Update([FromBody]UpdatePostDto post, int id)
        {
            return Ok(await _mediator.Send(new UpdatePostCommand(post, post.Interests, id)));
        }
        //related routes
        [HttpGet("{id}/comments")]
        public async Task<ActionResult<PagedResultDto<CommentQueryDto>>> GetPostComments(int id, string? orderBy, int pageNumber = 1)
        {
            var pageDto = new PageModel
            {
                PageSize = 10,
                PageNumber = pageNumber
            };
            return Ok(await _mediator.Send(new GetPostCommentsQuery(pageDto, id, orderBy)));
        }

        [HttpPost("{id}/comments")]
        [Authorize]
        public async Task<ActionResult<CommentQueryDto>> AddComment(CreateCommentDto comment, int id)
        {
            return Ok(await _mediator.Send(new CreateCommentCommand(comment, id)));
        }

        [HttpDelete("{id}/comments/{commentId}")]
        [Authorize]
        public async Task<ActionResult<CommentQueryDto>> RemoveComment(int commentId)
        {
            var result = await _mediator.Send(new RemoveCommentByIdCommand(commentId));
            return Ok(result);
        }

        [HttpGet("{id}/likes")]
        public async Task<ActionResult<IEnumerable<LikeQueryDto>>> GetPostLikes(int id)
        {
            return Ok(await _mediator.Send(new GetPostLikesQuery(id)));
        }

        [HttpPost("{id}/likes")]
        [Authorize]
        public async Task<ActionResult<LikeQueryDto>> AddLike(int id)
        {
            return Ok(await _mediator.Send(new CreateLikeCommand(id)));
        }

        [HttpDelete("{id}/likes")]
        [Authorize]
        public async Task<ActionResult<LikeQueryDto>> RemoveLike(int id)
        {
            var result = await _mediator.Send(new RemoveLikeCommand(id));
            return Ok(result);
        }

        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            BlobResponseDto? response = await _storage.UploadAsync(file);

            // Check if we got an error
            if (response.Error == true)
            {
                // We got an error during upload, return an error with details to the client
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                // Return a success message to the client about successfull upload
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }
    }
}
