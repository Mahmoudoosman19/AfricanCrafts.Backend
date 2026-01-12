namespace Common.Domain.Repositories;

public interface IOrderUnitOfWork
{
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
}
