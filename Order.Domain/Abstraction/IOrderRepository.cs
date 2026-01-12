using Common.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Abstraction
{
    public interface IOrderRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
    }
}
