using Order.Application.DTOs.CheckOut;
using Order.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Abstraction
{
    public interface IProductService
    {
        Task<List<ProductIntegrationDto>> GetProductsBulkAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
    }
}
