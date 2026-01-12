using Common.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Abstraction
{
    public interface IProductUnitOfWork : IOrderUnitOfWork
    {
    }
}
