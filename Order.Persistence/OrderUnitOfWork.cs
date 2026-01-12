using Common.Domain.Repositories;
using Order.Persistence.Repositories;
using System.Collections;

namespace Order.Persistence;

internal sealed class OrderUnitOfWork : IOrderUnitOfWork
{
    private readonly OrderDbContext _context;
    private Hashtable _repositories;

    public OrderUnitOfWork(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        if (_repositories is null)
            _repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repository = new OrderRepository<TEntity>(_context);
            _repositories.Add(type, repository);
        }

        return (IGenericRepository<TEntity>)_repositories[type]!;
    }
}
