using AutoMapper;
using Bloggr.Application.Interests.Commands.RemoveInterest;
using Bloggr.Application.Likes.Commands.CreateLike;
using Bloggr.Application.Likes.Queries.GetLikes;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public LikesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/<CommentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> Get()
        {
            var result = await _mediator.Send(new GetLikesQuery());
            return Ok(result);
        }

        // POST api/<CommentsController>
        //[HttpPost]
        //public async Task<ActionResult<Like>> Post([FromBody] AddLikeDTO like)
        //{
        //    var mappedLike = _mapper.Map<Like>(like);
        //    var result = await _mediator.Send(new CreateLikeCommand(mappedLike));
        //    return Ok(result);
        //}

        //// DELETE api/<CommentsController>/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Like>> Delete(int id)
        //{
        //    var result = await _mediator.Send(new RemoveInterestByIdCommand(id));
        //    return Ok(result);
        //}
    }
}
