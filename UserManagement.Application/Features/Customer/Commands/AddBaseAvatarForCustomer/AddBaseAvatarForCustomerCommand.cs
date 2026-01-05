using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Customer.Commands.AddBaseAvatarForCustomer
{
    public class AddBaseAvatarForCustomerCommand:ICommand<string>
    {
        public string BaseAvatarId { get; set; }
    }
}
