using Bus.Contracts.Catalog;
//using MassTransit;
using Product.Application.Specifications.Reviews;
using Product.Domain.DomainEvents;

namespace Product.Application.Features.Review.Events
{
    //internal class UpdateProductRateDomainEventHandler
    //    : IDomainEventHandler<UpdateProductRateDomainEvent>
    //{
    //    private readonly IGenericRepository<Domain.Entities.Review> _reviewRepo;
    //    private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
    //    private readonly IPublishEndpoint _publishEndpoint;

    //    public UpdateProductRateDomainEventHandler(
    //        IGenericRepository<Domain.Entities.Review> reviewRepo,
    //        IGenericRepository<Domain.Entities.Product> productRepo,
    //        IPublishEndpoint publishEndpoint)
    //    {
    //        _reviewRepo = reviewRepo;
    //        _productRepo = productRepo;
    //        _publishEndpoint = publishEndpoint;
    //    }
    //    public async Task Handle(UpdateProductRateDomainEvent notification, CancellationToken cancellationToken)
    //    {
    //        (var productReviews, int count) = _reviewRepo.GetWithSpec(new GetReviewByProductIdSpecification(notification.ProductId));

    //        var sumOfRatings = productReviews.Sum(x => x.Rate);

    //        double averageRating = (double)sumOfRatings / count;

    //        var product = await _productRepo.GetByIdAsync(notification.ProductId, cancellationToken);

    //        product!.SetRate(averageRating);

    //        _productRepo.Update(product);

    //        await _productRepo.SaveChangesAsync(cancellationToken);

    //        var msg = new UpdateProductRateContract(product.Id, product.Rate);

    //        await _publishEndpoint.Publish(msg, cancellationToken);
    //    }
    //}
}
