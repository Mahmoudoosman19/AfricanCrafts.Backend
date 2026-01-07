using Bus.Contracts.Catalog;
using Bus.Contracts.Models.Product;
//using MassTransit;
using Product.Application.Abstractions;
using Product.Application.Features.Product.Queries.GetProductDetails;
using Product.Application.Specifications.Products;
using Product.Application.Specifications.Reviews;
using Product.Domain.DomainEvents;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Events
{
    //internal sealed class ProductToggleActivationDomainEventHandler
    //    : IDomainEventHandler<ProductToggleActivationDomainEvent>
    //{
    //    private readonly IPublishEndpoint _publishEndpoint;
    //    private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
    //    private readonly IMapper _mapper;
    //    private readonly IVendorService _vendorService;
    //    private readonly IGenericRepository<Domain.Entities.Review> _reviewRepo;

    //    public ProductToggleActivationDomainEventHandler(
    //        IPublishEndpoint publishEndpoint,
    //        IGenericRepository<Domain.Entities.Product> productRepo,
    //        IMapper mapper,
    //        IVendorService vendorService,
    //        IGenericRepository<Domain.Entities.Review> reviewRepo)
    //    {
    //        _publishEndpoint = publishEndpoint;
    //        _productRepo = productRepo;
    //        _mapper = mapper;
    //        _vendorService = vendorService;
    //        _reviewRepo = reviewRepo;
    //    }
    //    public async Task Handle(ProductToggleActivationDomainEvent notification, CancellationToken cancellationToken)
    //    {
    //        if (notification.IsActive)
    //        {
    //            var specQuery = new GetProductDetailsByIdWithRelationsProductQuery() { Id = notification.ProductId };

    //            var product = _productRepo.GetEntityWithSpec(new GetProductDetailsByIdWithRelationsProductSpecification(specQuery));
    //            var reviewCount = _reviewRepo.GetWithSpec(new GetReviewByProductIdSpecification(notification.ProductId)).count;

    //            if (product == null || product.Status != Domain.Enums.ProductStatus.Approved || product.Extensions.Count == 0 || product.VendorId == Guid.Empty)
    //                return;

    //            var productModel = _mapper.Map<ProductModel>(product);
    //            productModel.NumberOfRatings = reviewCount;
    //            var vendor = await _vendorService.GetById(product.VendorId);
    //            productModel.Vendor = _mapper.Map<VendorModel>(vendor.Data);

    //            var msg = new AddProductContract(productModel);

    //            await _publishEndpoint.Publish(msg, cancellationToken);
    //        }
    //        else
    //        {
    //            var msg = new DeleteProductContract(notification.ProductId);

    //            await _publishEndpoint.Publish(msg, cancellationToken);
    //        }
    //    }
    //}
}
