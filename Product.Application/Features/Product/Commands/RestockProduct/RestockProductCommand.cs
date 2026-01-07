namespace Product.Application.Features.Product.Commands.RestockProduct
{
    public class RestockProductCommand : ICommand
    {
        public int Amount { get; set; }
        public Guid ProductExtensionId { get; set; }
        public bool Increase {  get; set; } 
    }
}
