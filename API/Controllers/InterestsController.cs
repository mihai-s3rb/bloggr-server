using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InterestsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InterestsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterestQueryDto>>> Get()
        {
            return Ok(await _mediator.Send(new GetInterestsQuery()));
        }
    }
}
