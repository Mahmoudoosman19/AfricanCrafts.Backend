namespace Order.Domain.Enum
{
    public enum OrderStatusEnum
    {
        Initiated = 1,
        Shipped,
        Delivered,
        Canceled,
        Returned,
        CustomerReceived,
        Pending,
        Refunded
    }
}
