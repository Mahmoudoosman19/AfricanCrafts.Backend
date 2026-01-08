using Product.Application.Specifications.SizeGroupQuestion;
using Product.Domain.Abstraction;

namespace Product.Application.Features.SizeGroupQuestions.Queries.GetListSizeGroupQuestion
{
    public class GetListSizeQuestionQueryHandler : IQueryHandler<GetListSizeQuestionQuery, IEnumerable<SizeQuestionResponse>>
    {

        private readonly IProductRepository<Domain.Entities.SizeGroupQuestion> _sizeQuestionRepo;
        private readonly IMapper _mapper;


        public GetListSizeQuestionQueryHandler(IProductRepository<Domain.Entities.SizeGroupQuestion> sizeQuestionRepo, IMapper mapper)
        {
            _sizeQuestionRepo = sizeQuestionRepo;
            _mapper = mapper;
        }
        public async Task<ResponseModel<IEnumerable<SizeQuestionResponse>>> Handle(GetListSizeQuestionQuery request, CancellationToken cancellationToken)

        {
            var (sizeQuery, count) = _sizeQuestionRepo.GetWithSpec(new SizeQuestionBySizeGroupIdWithSizeGroupSpecification(request));
            var sizequestions = _mapper.Map<IEnumerable<SizeQuestionResponse>>(sizeQuery);
            return ResponseModel.Success(sizequestions, count);
        }
    }
}
