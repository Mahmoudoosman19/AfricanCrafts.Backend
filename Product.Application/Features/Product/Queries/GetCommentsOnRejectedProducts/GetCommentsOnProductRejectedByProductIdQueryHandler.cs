using IdentityHelper.Abstraction;
using IdentityHelper.Models;
using Product.Application.Abstractions;
using Product.Application.Specifications.Products;

namespace Product.Application.Features.Product.Queries.GetCommentsOnRejectedProducts
{
    public class GetCommentsOnProductRejectedByProductIdQueryHandler : IQueryHandler<GetCommentsOnRejectedProductsByProductIdQuery, ProductsQueryResponse>
    {

        private readonly IGenericRepository<Domain.Entities.ProductComment> _productCommentRepo;
        private readonly IUserManagement _userManagement;
        private readonly IMapper _mapper;

        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        public GetCommentsOnProductRejectedByProductIdQueryHandler(
     IGenericRepository<Domain.Entities.ProductComment> productCommentRepo,
     IUserManagement userManagement,
     IGenericRepository<Domain.Entities.Product> productRepo,
     IMapper mapper)
        {

            _userManagement = userManagement;
            _productCommentRepo = productCommentRepo;

            _productRepo = productRepo;
            _mapper = mapper;
        }
        public async Task<ResponseModel<ProductsQueryResponse>> Handle(GetCommentsOnRejectedProductsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var commentsQuery = _productCommentRepo
                                    .GetWithSpec(new GetProductByProductIdAndCreateDetaSpecification(request)).data;
            if (commentsQuery.Any())
            {
                var response = await MappProductComment(commentsQuery.FirstOrDefault());
                response.Comments = _mapper.Map<List<CommentProductQueryResponse>>(commentsQuery);
                return ResponseModel.Success(response);
            }
            return ResponseModel.Success(new ProductsQueryResponse());



        }
        private async Task<ProductsQueryResponse> MappProductComment(Domain.Entities.ProductComment productComment)
        {
            var product = await _productRepo.GetByIdAsync(productComment.ProductId);
            UserModel? vendor = null;
            try
            {
                vendor = await _userManagement.GetUserData(product!.VendorId);
            }
            catch (Exception ex)
            {
            }
            var supervisor = await _userManagement.GetUserData(productComment.CreatedBy);

            return new ProductsQueryResponse()
            {
                productNameAr = product!.NameAr,
                productNameEn = product.NameEn,
                SupervisorNameAr = supervisor?.FullNameAr ?? "",
                SupervisorNameEn = supervisor?.FullNameEn ?? "",
                CreatedOnUtc = productComment.CreatedOnUtc,
            };
        }
    }
}
