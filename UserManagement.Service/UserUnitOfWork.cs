using Common.Domain.Repositories;
using System.Collections;
using UserManagement.Domain.Abstraction;
using UserManagement.Persistence.Repositories;

namespace UserManagement.Persistence;

internal sealed class UserUnitOfWork : IUserUnitOfWork
{
    private readonly UserManagementDbContext _context;
    private Hashtable _repositories;

    public UserUnitOfWork(UserManagementDbContext context)
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
            var repository = new UserRepository<TEntity>(_context);
            _repositories.Add(type, repository);
        }

        return (IGenericRepository<TEntity>)_repositories[type]!;
    }
}
