using Common.Domain.Repositories;
using Product.Domain.Abstraction;
using Product.Persistence.Repositories;
using System.Collections;

namespace Product.Persistence;

internal sealed class ProductUnitOfWork : IProductUnitOfWork
{
    private readonly ProductDbContext _context;
    private Hashtable _repositories;

    public ProductUnitOfWork(ProductDbContext context)
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
            var repository = new ProductRepository<TEntity>(_context);
            _repositories.Add(type, repository);
        }

        return (IGenericRepository<TEntity>)_repositories[type]!;
    }
}
