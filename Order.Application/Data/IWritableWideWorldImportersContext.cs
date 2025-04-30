namespace Order.Application.Data;

public interface IWritableWideWorldImportersContext : IWideWorldImportersContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}