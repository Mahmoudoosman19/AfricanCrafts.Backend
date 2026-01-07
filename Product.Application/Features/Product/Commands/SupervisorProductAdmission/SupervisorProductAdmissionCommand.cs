namespace Product.Application.Features.Product.Commands.SupervisorProductAdmission
{
    public class SupervisorProductAdmissionCommand: ICommand
    {
        public Guid ProductId { get; init; }
    }
}
