namespace Order.Application.DTOs.OrderDetails
{
    public class DashboardOrderDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; init; } = null!;
        public string ColorCode { get; init; } = null!;
        public string ProductImageUrl { get; init; } = null!;
        public string SizeName { get; init; } = null!;
        public string SizeDescription { get; init; } = null!;
        public decimal Price { get; init; }
        public int Quantity { get; set; }
    }
}
