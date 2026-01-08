using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeTable.Commands.CreatesSizeTable
{

    public class CreatesQuestionsCommandHandler
   : ICommandHandler<CreatesQuestionsCommand>
    {
        private readonly IProductRepository<SizeGroupQuestion> _questionsRepo;
        private readonly IProductRepository<SizeGroup>_sizeGroupRepo;
        private readonly IMapper _mapper;

        public CreatesQuestionsCommandHandler(
            IProductRepository<SizeGroupQuestion> questionsRepo,
            IProductRepository<SizeGroup> sizeGroupRepo,
            IMapper mapper)
        {
            _questionsRepo = questionsRepo;
            _sizeGroupRepo = sizeGroupRepo;  
            _mapper = mapper;
        }

        public async Task<ResponseModel> Handle(CreatesQuestionsCommand request, CancellationToken cancellationToken)
        {
            var sizeGroup =await _sizeGroupRepo.GetByIdAsync(request.SizeGroupId);
            sizeGroup.AddRangeQuestions(_mapper.Map<List<SizeGroupQuestion>>(request.Questions));   
            await _sizeGroupRepo .SaveChangesAsync(cancellationToken);
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
