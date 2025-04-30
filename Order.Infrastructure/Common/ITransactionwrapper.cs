using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.Common;

public interface ITransactionWrapper
{
    Task ExecuteAsync<T1>(T1 dbContext, Func<CancellationToken, Task> action, CancellationToken cancellationToken)
        where T1 : DbContext;
}

public class TransactionWrapper : ITransactionWrapper
{
    public async Task ExecuteAsync<T1>(T1 dbContext, Func<CancellationToken, Task> action, CancellationToken cancellationToken) where T1 : DbContext
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction1 = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action(cancellationToken);

                await transaction1.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction1.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }
}