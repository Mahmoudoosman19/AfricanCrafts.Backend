namespace Product.Application.Features.Product.Commands.ChangeProductActivation;

public sealed record ToggleProductActivationCommand : ICommand
{
    public Guid ProductId { get; init; }
}
