namespace Product.Application.Features.Sizes.Commands.ToggleSizeStop
{
    public sealed record ToggleSizeActivationCommand : ICommand
    {
        public Guid SizeId { get; init; }

    }
}
