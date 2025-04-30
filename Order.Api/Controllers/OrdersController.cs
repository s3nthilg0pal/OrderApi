using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Sales;
using Order.Application.Sales.Orders;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetail))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<OrderDetail>> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery
            {
                OrderId = id
            };

            var response = await _mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            
            return Ok(response);
        }
        
        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<Domain.Models.Order>>> GetMostRecentOrders([FromQuery] int days = 30)
        {
            var query = new GetMostRecentOrdersQuery
            {
                Days = days
            };

            var orders = await _mediator.Send(query);

            if (!orders.Any())
            {
                return NoContent();
            }

            return Ok(orders);

        }


        [HttpGet("items/topSelling")]
        public async Task<ActionResult<IEnumerable<GetTopSellingItemsQueryHandler.MostSellingItem>>>
            GetMostSellingItem()
        {
            var query = new GetTopSellingItemsQuery();

            var itesm = await _mediator.Send(query);

            return Ok(itesm);
        }

        [HttpGet("suppliers/inactive")]
        public async Task<ActionResult<IEnumerable<SupplierInfo>>> GetInactiveSuppliers()
        {
            var query = new GetInactiveSuppliersQuery();
            var respose = await _mediator.Send(query);

            return Ok(respose);
        }
    }
}
