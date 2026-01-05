
namespace UserManagement.Application.Features.Customer.Commands.AddRefundProofImg
{
    internal class AddRefundProofImgCommandValidator : AbstractValidator<AddRefundProofImgCommand>
    {
        public AddRefundProofImgCommandValidator()
        {
            RuleFor(product => product.OrderId)
                .NotEmpty().WithMessage(Messages.EmptyField);
            
            
            RuleFor(product => product.RefundProofImgUrl)
                .NotEmpty().WithMessage(Messages.EmptyField);
        }
    }
}
