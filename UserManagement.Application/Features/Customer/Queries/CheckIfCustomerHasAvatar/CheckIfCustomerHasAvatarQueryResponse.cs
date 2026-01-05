namespace UserManagement.Application.Features.Customer.Queries.CheckIfCustomerHasAvatar
{
    internal class CheckIfCustomerHasAvatarQueryResponse
    {
        public bool CustomerHasAvatar { get; set; }
        public string CustomerBaseAvatarUrl { get; set; }
        public string? CustomerCustomizedAvatarUrl { get; set; }

    }
}
