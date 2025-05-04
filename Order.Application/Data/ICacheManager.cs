namespace Order.Application.Data;

public interface ICacheManager
{
    Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, ValueTask<T>> action, IEnumerable<string> tags = default, CancellationToken cancellationToken = default);
}