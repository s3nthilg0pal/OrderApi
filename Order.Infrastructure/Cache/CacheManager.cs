using Microsoft.Extensions.Caching.Hybrid;
using Order.Application.Data;

namespace Order.Infrastructure.Cache;

public class CacheManager : ICacheManager
{
    private readonly HybridCache _cache;

    public CacheManager(HybridCache cache)
    {
        _cache = cache;
    }
    public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, ValueTask<T>> action, IEnumerable<string> tags, CancellationToken cancellationToken = default)
    {
        var entryOptions = new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(1),
            LocalCacheExpiration = TimeSpan.FromMinutes(1)
        };
        return await _cache.GetOrCreateAsync(key,  action, entryOptions,tags, cancellationToken);
    }
}