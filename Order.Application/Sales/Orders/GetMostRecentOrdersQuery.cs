using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Data;

namespace Order.Application.Sales.Orders;

public record GetMostRecentOrdersQuery : IRequest<IEnumerable<Domain.Models.Order>>
{
    public int Days { get; set; }

}

public class GetMostRecentOrdersQueryHandler : IRequestHandler<GetMostRecentOrdersQuery, IEnumerable<Domain.Models.Order>>
{
    private readonly IWideWorldImportersContext _context;

    public GetMostRecentOrdersQueryHandler(IWideWorldImportersContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Domain.Models.Order>> Handle(GetMostRecentOrdersQuery request, CancellationToken cancellationToken)
    {
        var from =DateOnly.FromDateTime(DateTime.Now.AddDays(-request.Days));
        var to = DateOnly.FromDateTime(DateTime.Now);
        var recentOrders = await _context.Orders.Where(x => x.OrderDate >= from && x.OrderDate < to)
            .ToListAsync(cancellationToken);
        return recentOrders;
    }
}