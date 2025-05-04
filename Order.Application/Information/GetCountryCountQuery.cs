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
    private readonly ICacheManager _cacheManager;

    public GetCountryCountQueryHandler(IApplicationDbContext context, ICacheManager cacheManager)
    {
        _context = context;
        _cacheManager = cacheManager;
    }
    public async Task<List<CountryCountDto>> Handle(GetCountryCountQuery request, CancellationToken cancellationToken)
    {
        var countryCount = await _cacheManager
            .GetOrCreateAsync("countryCount",  GetCountryCountAsync, cancellationToken: cancellationToken);
        return countryCount;
    }

    private async ValueTask<List<CountryCountDto>> GetCountryCountAsync(CancellationToken cancellationToken)
    {
        var countryCount = await _context.StateProvinces
            .GroupBy(c => c.Country)
            .Select(c => new CountryCountDto
            {
                CountryId = c.Key.CountryId,
                Count = c.Count()
            }).ToListAsync(cancellationToken);

        return countryCount;
    }
}