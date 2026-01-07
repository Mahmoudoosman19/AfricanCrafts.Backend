namespace Product.Application.Features.Product.Commands.SupervisorDeletesComment
{
    public class DeletesCommentHandler : ICommandHandler<DeletesCommentCommand>
    {
        private readonly IGenericRepository<Domain.Entities.ProductComment> _commentRepo;
        public DeletesCommentHandler(IGenericRepository<Domain.Entities.ProductComment> commentRepo)
        {
            _commentRepo = commentRepo;
        }
        public async Task<ResponseModel> Handle(DeletesCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepo.GetByIdAsync(request.ProductCommentId);

            _commentRepo.Delete(comment!);
            await _commentRepo.SaveChangesAsync();
            return ResponseModel
                .Success(Messages.SuccessfulOperation);
        }
    }
}
