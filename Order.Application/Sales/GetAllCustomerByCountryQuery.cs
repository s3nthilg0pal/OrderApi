using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Data;
using Order.Domain.Models;

namespace Order.Application.Sales;

public record GetAllCustomerByCountryQuery : IRequest<IEnumerable<Customer>>
{
    public string Country { get; set; } = null!;
}

public class GetAllCustomerByCountryQueryHandler : IRequestHandler<GetAllCustomerByCountryQuery, IEnumerable<Customer>>
{
    private readonly IWideWorldImportersContext _context;

    public GetAllCustomerByCountryQueryHandler(IWideWorldImportersContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Customer>> Handle(GetAllCustomerByCountryQuery request, CancellationToken cancellationToken)
    {
        var customers = await _context.Countries
            .Include(x => x.StateProvinces)
            .ThenInclude(x => x.Cities)
            .Where(x => x.CountryName.ToLower() == request.Country.ToLower())
            .Select(x => x.StateProvinces
                .SelectMany(x => x.Cities)
                .SelectMany(x => x.CustomerDeliveryCities)
            ).FirstOrDefaultAsync(cancellationToken);

        return customers;
    }
}