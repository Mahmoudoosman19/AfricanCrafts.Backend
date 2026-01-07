namespace Product.Domain.Options
{
    public class GRPCOptions
    {
        public string UserManagementChannelAddress { get; set; } = null!;
        public string OrderChannelAddress { get; set; } = null!;
    }
}
