namespace Product.Application.Features.Product.Queries.IsProductExist
{
    public class ProductExistQuery : IQuery<bool>
    {
        public Guid Id { get; set; }
    }
}
