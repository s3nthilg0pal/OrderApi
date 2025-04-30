using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Data;

namespace Order.Application.Sales;

public record GetInactiveSuppliersQuery() : IRequest<IEnumerable<SupplierInfo>>;

public class SupplierInfo
{
    public string Name { get; set; }
}

public class GetInactiveSupplierQueryHandler : IRequestHandler<GetInactiveSuppliersQuery, IEnumerable<SupplierInfo>>
{
    private readonly IWideWorldImportersContext _context;

    public GetInactiveSupplierQueryHandler(IWideWorldImportersContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<SupplierInfo>> Handle(GetInactiveSuppliersQuery request, CancellationToken cancellationToken)
    {
        var inactiveSuppliers = await _context.Suppliers.Where(x => !x.SupplierTransactions.Any()).Select(x =>
            new SupplierInfo()
            {
                Name = x.SupplierName
            }).ToListAsync(cancellationToken);
        return inactiveSuppliers;
    }
}