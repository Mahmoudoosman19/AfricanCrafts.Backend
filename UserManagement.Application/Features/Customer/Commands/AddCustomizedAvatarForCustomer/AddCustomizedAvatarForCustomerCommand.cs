using Common.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace UserManagement.Application.Features.Customer.Commands.AddCustomizedAvatarForCustomer
{
    public class AddCustomizedAvatarForCustomerCommand:ICommand<string>
    {
        public string BaseAvatarId { get;  set; }
        public IFormFile CustomizedAvatar { get;  set; }
    }
}
