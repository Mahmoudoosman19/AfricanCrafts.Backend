using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.OrderUser.Queries.GetPendingCancellationOrders
{
    public class GetPendingCancellationOrdersQuery : IQuery<List<GetPendingCancellationOrdersResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
    }
}
