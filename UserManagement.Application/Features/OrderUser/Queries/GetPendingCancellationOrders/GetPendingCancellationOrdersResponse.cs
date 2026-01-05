namespace UserManagement.Application.Features.OrderUser.Queries.GetPendingCancellationOrders
{
    public class GetPendingCancellationOrdersResponse
    {
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public DateTime DateOfRequest { get; set; }

        public string StatusName { get; set; }
        public string ReturnedReason { get; set; }
    }
}
