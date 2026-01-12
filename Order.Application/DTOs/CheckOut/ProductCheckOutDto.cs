namespace Order.Application.DTOs.CheckOut
{
    public class ProductCheckOutDto
    {
        public Guid ProductId { get; set; }
        public Guid ProductExtensionId { get; set; }
        public Guid VendorId { get; set; }
        public int Quantity { get; set; }
    }
}
