
using UserManagement.Application.SharedDTOs;

namespace UserManagement.Application.Abstractions
{
    public interface IOrderUserService
    {
     Task<List<PendingCancellationOrdersResponseDto>> GetAllPendingCancellationOrders();
    }
}
