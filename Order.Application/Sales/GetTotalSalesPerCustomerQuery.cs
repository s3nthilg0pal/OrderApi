using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Data;

namespace Order.Application.Sales;

public record GetTotalSalesPerCustomerQuery() : IRequest<IEnumerable<CustomerSales>>;

public record CustomerSales
{
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalSales { get; set; }
}

public class GetTotalSalesPerCustomerQueryHandler : IRequestHandler<GetTotalSalesPerCustomerQuery, IEnumerable<CustomerSales>>
{
    private readonly IWideWorldImportersContext _context;

    public GetTotalSalesPerCustomerQueryHandler(IWideWorldImportersContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<CustomerSales>> Handle(GetTotalSalesPerCustomerQuery request, CancellationToken cancellationToken)
    {
        var customers = await _context.Invoices.Select(x => new CustomerSales()
        {
            CustomerName = x.Customer.CustomerName,
            TotalSales = x.InvoiceLines.Sum(il => il.UnitPrice.Value)
        }).ToListAsync(cancellationToken);

        return customers;
    }
}