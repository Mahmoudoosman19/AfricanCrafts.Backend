namespace Order.Application.DTOs.Order
{
    public sealed class OrderStatusDto
    {
        public long Id { get; set; }
        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;
    }
}
