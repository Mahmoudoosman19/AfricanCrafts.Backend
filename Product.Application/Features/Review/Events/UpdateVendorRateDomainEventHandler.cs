using Bus.Contracts.Catalog;
//using MassTransit;
using Product.Application.Specifications.Products;
using Product.Domain.DomainEvents;

namespace Product.Application.Features.Review.Events
{
    //internal class UpdateVendorRateDomainEventHandler
    //    : IDomainEventHandler<UpdateVendorRateDomainEvent>
    //{
    //    private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
    //    private readonly IPublishEndpoint _publishEndpoint;

    //    public UpdateVendorRateDomainEventHandler(
    //        IGenericRepository<Domain.Entities.Product> productRepo,
    //        IPublishEndpoint publishEndpoint)
    //    {
    //        _productRepo = productRepo;
    //        _publishEndpoint = publishEndpoint;
    //    }
    //    public async Task Handle(UpdateVendorRateDomainEvent notification, CancellationToken cancellationToken)
    //    {
    //        (var vendorProducts, int count) = _productRepo.GetWithSpec(new GetProductByVendorIdWithImageSpecification(notification.VendorId));

    //        var sumOfProductsRatings = vendorProducts.Sum(x => x.Rate);

    //        double averageRating = (double)sumOfProductsRatings / count;

    //        var msg = new UpdateVendorRateContract(notification.VendorId, averageRating);

    //        await _publishEndpoint.Publish(msg, cancellationToken);
    //    }
    //}
}
