using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Data;

namespace Order.Application.Sales.Orders;

public record GetOrderByIdQuery : IRequest<OrderDetail>
{
    public int OrderId { get; set; }
}

public class GetOrderByIdQueryHandler(IWideWorldImportersContext context) : IRequestHandler<GetOrderByIdQuery, OrderDetail>
{

    public async Task<OrderDetail> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderLines)
            .Include(o => o.Invoices)
            .Where(o => o.OrderId == request.OrderId)
            .Select(o => new OrderDetail(o.Customer.CustomerName, 
                o.OrderLines.Select(x => x.Description).ToArray(),
                o.Invoices.Select(x => x.InvoiceId).ToArray(),o.ExpectedDeliveryDate, o.OrderDate)).FirstOrDefaultAsync(cancellationToken);
        return order;
    }
}