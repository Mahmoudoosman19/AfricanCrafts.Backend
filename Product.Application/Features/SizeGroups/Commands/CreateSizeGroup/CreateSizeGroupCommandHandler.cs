using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Commands.CreateSizeGroup
{
    internal class CreateSizeGroupCommandHandler
        : ICommandHandler<CreateSizeGroupCommand>
    {
        private readonly IProductRepository<SizeGroup> _sizeGroupsRepository;
        private readonly IMapper _mapper;

        public CreateSizeGroupCommandHandler(
            IProductRepository<SizeGroup> sizeGroupsRepository,
            IMapper mapper)
        {
            _sizeGroupsRepository = sizeGroupsRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel> Handle(CreateSizeGroupCommand request, CancellationToken cancellationToken)
        {
            var sizeGroup = _mapper.Map<SizeGroup>(request);

            sizeGroup.AddRangeSize(_mapper.Map<List<Size>>(request.Sizes));
            
            sizeGroup.AddRangeQuestions(_mapper.Map<List<SizeGroupQuestion>>(request.Questions));

            await _sizeGroupsRepository.AddAsync(sizeGroup, cancellationToken);
            await _sizeGroupsRepository.SaveChangesAsync(cancellationToken);

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}