using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Data;

namespace Order.Application.Information;

public class GetCountryCountQuery : IRequest<List<CountryCountDto>>
{
    
}

public class GetCountryCountQueryHandler : IRequestHandler<GetCountryCountQuery, List<CountryCountDto>>
{
    private readonly IApplicationDbContext _context;
    public GetCountryCountQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<CountryCountDto>> Handle(GetCountryCountQuery request, CancellationToken cancellationToken)
    {
        var countryCount = await _context.StateProvinces.AsNoTracking()
            
            .CountBy(c => c.Country.CountryId)
            .Select(c => new CountryCountDto
            {
                CountryId  = c.Key,
                Count = c.Value
            }).ToListAsync(cancellationToken);
        return countryCount;
    }
}