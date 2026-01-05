using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Wallet.Command.DistributeProfitShare
{
    public class DistributeProfitShareCommand : IQuery<bool>
    {
        public List<DistributeProfitShare> Commands { get; set; }
    }

    public class DistributeProfitShare
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }
        public Guid VendorId { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public long PaymentStatusId { get; set; }
    }
}
