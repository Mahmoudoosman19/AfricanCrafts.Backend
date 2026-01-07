using IdentityHelper.Abstraction;
using Product.Application.Specifications.Advertisement;

namespace Product.Application.Features.Advertisements.Queries.GetList
{
    public class GetAdvertisementBaseStatusAndRoleQueryHandler : IQueryHandler<GetAdvertisementBaseStatusAndRoleQuery, IEnumerable<AdvertisementQueryResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Advertisement> _advertisementRepo;
        private readonly IMapper _mapper;
        private readonly ITokenExtractor _tokenExtractor;

        public GetAdvertisementBaseStatusAndRoleQueryHandler(
            IGenericRepository<Domain.Entities.Advertisement> repository,
            IMapper mapper,
            ITokenExtractor tokenExtractor)
        {
            _advertisementRepo = repository;
            _mapper = mapper;
            _tokenExtractor = tokenExtractor;
        }

        public Task<ResponseModel<IEnumerable<AdvertisementQueryResponse>>> Handle(GetAdvertisementBaseStatusAndRoleQuery request, CancellationToken cancellationToken)
        {
            var userRole = _tokenExtractor.GetUserRole();
            bool isAdmin = userRole == "Admin";
            var (listQuery, count) = _advertisementRepo.GetWithSpec(new FilterAdvertisementBaseStatusAndRoleSpecification(request, isAdmin));
            var advertisement = _mapper.Map<IEnumerable<AdvertisementQueryResponse>>(listQuery);
            return Task.FromResult(ResponseModel.Success(advertisement, count));
        }
    }
}
