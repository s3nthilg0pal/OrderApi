using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Mappers;
using Order.Application.Sales;
using Order.Domain.Models;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IMediator mediator, ILogger<CustomersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Results<Ok<PaginatedListDto<CustomerDto>>, NoContent>> GetAllCustomersWithPagination(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAllCustomersWithPaginationQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var customers =  await _mediator.Send(query, cancellationToken);

            var response = new PaginatedListDto<CustomerDto>
            {
                Items = customers.Items.Select(c => new CustomerDto()
                {
                    CustomerName = c.CustomerName
                }).ToList(),
                CurrentPage = customers.PageNumber,
                TotalPages = customers.TotalPages,
                TotalCount = customers.TotalCount,
                HasNextPage = customers.HasNextPage,
                HasPreviousPage = customers.HasPreviousPage
            };
            _logger.LogInformation("this is the paginated list");
            return TypedResults.Ok(response);
        }

        [HttpGet("{countryName}")]
        public async Task<Results<Ok<IEnumerable<CustomerDto>>, NoContent>> GetCustomersByCountryAsync(string countryName)
        {
            var query = new GetAllCustomerByCountryQuery()
            {
                Country = countryName
            };

            var customers = await _mediator.Send(query);
            var response = customers.Select(x => new CustomerDto()
            {
                CustomerName = x.CustomerName
            });

            return TypedResults.Ok(response);
        }

        [HttpGet("totalSales")]
        public async Task<Results<Ok<IEnumerable<CustomerSales>>, NoContent>> GetSalesByCustomersAsync()
        {
            var query = new GetTotalSalesPerCustomerQuery();
            var response = await _mediator.Send(query);
            return TypedResults.Ok(response);
        }
    }

   
}
