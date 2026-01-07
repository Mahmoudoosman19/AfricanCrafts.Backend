namespace Product.Application.Features.Product.Commands.UpdateProduct.UpdateProductHandlers
{
    public sealed class UpdateProductInput
    {
        public UpdateProductInput(UpdateProductCommand request, Domain.Entities.Product dbProduct, CancellationToken cancellationToken)
        {
            Request = request;
            DBProduct = dbProduct;
            CancellationToken = cancellationToken;
        }

        public UpdateProductCommand Request { get; init; }
        public Domain.Entities.Product DBProduct { get; init; }
        public CancellationToken CancellationToken { get; set; }
    }
}
