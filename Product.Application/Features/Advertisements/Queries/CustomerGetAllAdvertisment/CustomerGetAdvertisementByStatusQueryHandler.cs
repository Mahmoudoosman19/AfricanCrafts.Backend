using Product.Application.Specifications.Advertisement;
using Product.Domain.Abstraction;

namespace Product.Application.Features.Advertisements.Queries.CustomerGetAllAdvertisment
{
    public class CustomerGetAdvertisementByStatusQueryHandler : IQueryHandler<CustomerGetAdvertisementByStatusQuery, IEnumerable<CustomerAdvertisementsQueryResponse>>
    {
        private readonly IProductRepository<Domain.Entities.Advertisement> _advertisementRepo;
        private readonly IMapper _mapper;

        public CustomerGetAdvertisementByStatusQueryHandler(
            IProductRepository<Domain.Entities.Advertisement> advertisementRepo,
            IMapper mapper)
        {
            _advertisementRepo = advertisementRepo;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<CustomerAdvertisementsQueryResponse>>> Handle(CustomerGetAdvertisementByStatusQuery request, CancellationToken cancellationToken)
        {
            var specification = new CustomerGetAdvertisementByStatusSpecification(request);
            var (listQuery, count) = _advertisementRepo.GetWithSpec(specification);

            var advertisements = _mapper.Map<IEnumerable<CustomerAdvertisementsQueryResponse>>(listQuery);
            return ResponseModel.Success(advertisements, count);
        }
    }

}
