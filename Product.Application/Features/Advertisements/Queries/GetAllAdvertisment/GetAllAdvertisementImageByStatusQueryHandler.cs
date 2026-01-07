using Product.Application.Specifications.Advertisement;

namespace Product.Application.Features.Advertisements.Queries.GetAllAdvertisment
{
    public class GetAllAdvertisementImageByStatusQueryHandler : IQueryHandler<GetAllAdvertisementImageByStatusQuery, IEnumerable<GetAllAdvertismentImageResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.Advertisement> _advertisementRepo;
        private readonly IMapper _mapper;


        public GetAllAdvertisementImageByStatusQueryHandler(IGenericRepository<Domain.Entities.Advertisement> repository, IMapper mapper)
        {
            _advertisementRepo = repository;
            _mapper = mapper;
        }
        public Task<ResponseModel<IEnumerable<GetAllAdvertismentImageResponse>>> Handle(GetAllAdvertisementImageByStatusQuery request, CancellationToken cancellationToken)
        {
            var (listQuery, count) = _advertisementRepo.GetWithSpec(new GetAllAdvertisementImageByStatusSpecification(request));
            var advertisement = _mapper.Map<IEnumerable<GetAllAdvertismentImageResponse>>(listQuery);
            return Task.FromResult(ResponseModel.Success(advertisement, count));
        }
    }
}
