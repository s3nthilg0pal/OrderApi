using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Data;
using static Order.Application.Sales.GetTopSellingItemsQueryHandler;

namespace Order.Application.Sales;

public record GetTopSellingItemsQuery : IRequest<IEnumerable<GetTopSellingItemsQueryHandler.MostSellingItem>>
{
    
}

public class GetTopSellingItemsQueryHandler : IRequestHandler<GetTopSellingItemsQuery, IEnumerable<MostSellingItem>>
{
    private readonly IWideWorldImportersContext _context;

    public GetTopSellingItemsQueryHandler(IWideWorldImportersContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<MostSellingItem>> Handle(GetTopSellingItemsQuery request, CancellationToken cancellationToken)
    {
        var mosrSellingOrders = await _context.OrderLines.GroupBy(x => x.StockItem.StockItemName).Select(x => new MostSellingItem() {Item = x.Key, Count = x.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToListAsync(cancellationToken);

        return mosrSellingOrders;

    }

    public record MostSellingItem
    {
        public string Item { get; set; }
        public int Count { get; set; }
    }
}