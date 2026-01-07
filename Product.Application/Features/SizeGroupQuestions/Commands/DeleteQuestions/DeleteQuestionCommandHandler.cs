using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroupQuestions.Commands.DeleteQuestions
{
    public class DeleteQuestionCommandHandler : ICommandHandler<DeleteQuestionCommand>
    {
        private readonly IGenericRepository<SizeGroupQuestion> _questionsRepo;
        //private readonly IGenericRepository<SizeGroup> _sizeGroupRepo;
        private readonly IMapper _mapper;
        public DeleteQuestionCommandHandler(IGenericRepository<SizeGroupQuestion> questionsRepo, 
            IGenericRepository<SizeGroup> sizeGroupRepo)
        {
            _questionsRepo = questionsRepo;
        }
        public async Task<ResponseModel> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
           var Question = await _questionsRepo.GetByIdAsync(request.SizeGroupQuestionId);
            _questionsRepo.Delete(Question);

            await _questionsRepo.SaveChangesAsync();

            return ResponseModel
                .Success(Messages.SuccessfulOperation);
        }
    }
}
