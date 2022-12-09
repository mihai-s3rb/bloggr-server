using AutoMapper;
using Bloggr.Application.Comments.Commands.CreateComment;
using Bloggr.Application.Comments.Commands.RemoveComment;
using Bloggr.Application.Comments.Queries.GetComments;
using Bloggr.Application.Models.Comment;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CommentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/<CommentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> Get()
        {
            var result = await _mediator.Send(new GetCommentsQuery());
            return Ok(result);
        }

        // POST api/<CommentsController>
        [HttpPost]
        public async Task<ActionResult<Comment>> Post([FromBody] AddCommentDTO comment)
        {
            var mappedComment = _mapper.Map<Comment>(comment);
            var result = await _mediator.Send(new CreateCommentCommand(mappedComment));
            return result;
        }

        // PUT api/<CommentsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> Delete(int id)
        {
            var result = await _mediator.Send(new RemoveCommentByIdCommand(id));
            return Ok(result);
        }
    }
}
