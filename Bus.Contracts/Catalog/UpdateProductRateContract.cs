namespace Bus.Contracts.Catalog
{
    public sealed record UpdateProductRateContract(Guid ProductId, double Rate);
}
