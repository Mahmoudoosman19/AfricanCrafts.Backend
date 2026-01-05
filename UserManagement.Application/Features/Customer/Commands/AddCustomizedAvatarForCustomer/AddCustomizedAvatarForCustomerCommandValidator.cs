namespace UserManagement.Application.Features.Customer.Commands.AddCustomizedAvatarForCustomer
{
    internal class AddCustomizedRefundProofForCustomerCommandValidator : AbstractValidator<AddCustomizedAvatarForCustomerCommand>
    {
        public AddCustomizedRefundProofForCustomerCommandValidator()
        {
            RuleFor(product => product.BaseAvatarId)
                .NotEmpty().WithMessage(Messages.EmptyField);
            
            
            RuleFor(product => product.CustomizedAvatar)
                .NotEmpty().WithMessage(Messages.EmptyField);
        }
    }
}
