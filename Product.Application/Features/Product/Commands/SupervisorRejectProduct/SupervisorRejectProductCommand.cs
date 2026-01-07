namespace Product.Application.Features.Product.Commands.SupervisorRejectProduct;

public class SupervisorRejectProductCommand: ICommand
{
    public Guid Id { get; set; }
    public string Comment { get; set; } = null!;
}