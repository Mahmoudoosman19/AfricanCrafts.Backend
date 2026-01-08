using Common.Domain.Primitives;
using Common.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Abstraction
{
    public interface IUserRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
    }
}
