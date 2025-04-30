using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Information;
using Order.Application.Sales.Orders;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InformationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("countries/count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]

        public async Task<ActionResult<IEnumerable<CountryCountDto>>> GetCountriesCount()
        {
            var query = new GetCountryCountQuery();
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
