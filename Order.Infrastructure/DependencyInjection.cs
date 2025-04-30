using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Data;
using Order.Infrastructure.Data;

namespace Order.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<WideWorldImportersContext>(options => { options.UseSqlServer(connectionString); });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<WideWorldImportersContext>());

        services.AddScoped<IWideWorldImportersContext>(provider =>
            provider.GetRequiredService<WideWorldImportersContext>());
        
        services.AddScoped<IWritableWideWorldImportersContext>(provider =>
            provider.GetRequiredService<WideWorldImportersContext>());
        
        return services;
    }
}