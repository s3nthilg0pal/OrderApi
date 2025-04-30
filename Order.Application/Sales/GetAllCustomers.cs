using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Common;
using Order.Application.Data;
using Order.Domain.Models;

namespace Order.Application.Sales;

public record GetAllCustomersWithPaginationQuery : IRequest<PaginatedList<Customer>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetAllCustomersWithPaginationHandler(IWideWorldImportersContext context)
    : IRequestHandler<GetAllCustomersWithPaginationQuery, PaginatedList<Customer>>
{
    public async Task<PaginatedList<Customer>> Handle(GetAllCustomersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var customers =  context.Customers.AsNoTracking();

        var paginatedCustomers = await PaginatedList<Customer>.CreateAsync(customers, request.PageNumber, request.PageSize, cancellationToken);

        return paginatedCustomers;
    }
}