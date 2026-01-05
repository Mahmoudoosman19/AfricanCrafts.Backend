using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Customer.Queries.CheckIfCustomerHasAvatar
{
    public class CheckIfCustomerHasAvatarQuery : IQuery<CheckIfCustomerHasAvatarQueryResponse>
    {
        public Guid? userId { get; set; }
    }
}
