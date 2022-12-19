using AutoMapper;
using Bloggr.Application.Interests.Commands.CreateInterest;
using Bloggr.Application.Interests.Commands.RemoveInterest;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Models.Interest;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public InterestsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/<CommentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interest>>> Get()
        {
            var result = await _mediator.Send(new GetInterestsQuery());
            return Ok(result);
        }

        // POST api/<CommentsController>
        [HttpPost]
        public async Task<ActionResult<Interest>> Post([FromBody] AddInterestDTO interest)
        {
            var mappedInterest = _mapper.Map<Interest>(interest);
            var result = await _mediator.Send(new CreateInterestCommand(mappedInterest));
            return result;
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Interest>> Delete(int id)
        {
            var result = await _mediator.Send(new RemoveInterestByIdCommand(id));
            return Ok(result);
        }
    }
}
