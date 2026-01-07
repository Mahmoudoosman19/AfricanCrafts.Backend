namespace Bus.Contracts.Catalog
{
    public sealed record UpdateVendorRateContract(Guid VendorId, double Rate);
}
