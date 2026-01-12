namespace Order.Application.DTOs.OrderDetails
{
    public class OrderListResponse
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public string PaymentStatusNameEn { get; set; }
        public string PaymentStatusNameAr { get; set; }
        public string PaymentMethodNameAr { get; set; }
        public string PaymentMethodNameEn { get; set; }
        public string CustomerNameAr { get; set; }
        public string CustomerNameEn { get; set; }
        public string? StatusNameAr { get; set; }
        public string? StatusNameEn { get; set; }
        public bool IsShippingConfirms { get; set; }
    }
}
