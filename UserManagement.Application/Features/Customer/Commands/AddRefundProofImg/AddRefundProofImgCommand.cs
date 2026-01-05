using Common.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace UserManagement.Application.Features.Customer.Commands.AddRefundProofImg
{
    public class AddRefundProofImgCommand:ICommand<string>
    {
        public Guid OrderId { get;  set; }
        public IFormFile RefundProofImgUrl { get;  set; }
    }
}
