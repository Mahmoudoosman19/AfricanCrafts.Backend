namespace UserManagement.Application.Features.Customer.Commands.AddBaseAvatarForCustomer
{
    internal class AddBaseAvatarForCustomerValidator:AbstractValidator<AddBaseAvatarForCustomerCommand>
    {
        public AddBaseAvatarForCustomerValidator()
        {
            RuleFor(product => product.BaseAvatarId)
                .NotEmpty().WithMessage(Messages.EmptyField);
        }
    }
}
