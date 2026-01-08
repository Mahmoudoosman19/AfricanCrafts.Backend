using Product.Domain.Abstraction;

namespace Product.Application.Features.Advertisement.Command.ManageAdvertisementActivation
{
    public class ManageAdvertisementActivationCommandHandler : ICommandHandler<ManageAdvertisementActivationCommand>
    {
        private readonly IProductRepository<Domain.Entities.Advertisement> _advertisementRepository;
        private readonly IMapper _IMapper;


        public ManageAdvertisementActivationCommandHandler(IProductRepository<Domain.Entities.Advertisement> repository, IMapper mapper)
        {
            _advertisementRepository = repository;
            _IMapper = mapper;
        }

        public async Task<ResponseModel> Handle(ManageAdvertisementActivationCommand request, CancellationToken cancellationToken)
        {
            var advertisement = await _advertisementRepository.GetByIdAsync(request.Id);
            advertisement!.SetActivation(!advertisement.IsActive);
            await _advertisementRepository.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}

