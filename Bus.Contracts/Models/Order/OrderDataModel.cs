namespace Bus.Contracts.Models.Order
{
    public class OrderDataModel
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }
        public Guid VendorId { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public long PaymentStatusId { get; set; }
    }
}
