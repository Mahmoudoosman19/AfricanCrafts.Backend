using IdentityHelper.Abstraction;

namespace Product.Application.Features.Product.Commands.SupervisorDeletesComment
{
    public class DeletesCommentValidator:AbstractValidator<DeletesCommentCommand>
    {
        private readonly ITokenExtractor _tokenExtractor;
        private readonly IUserManagement _userManagement;
        private readonly IGenericRepository<Domain.Entities.ProductComment> _commentRepo;
        public DeletesCommentValidator(ITokenExtractor tokenExtractor, 
            IUserManagement userManagement,
            IGenericRepository<Domain.Entities.ProductComment> commentRepo)
        {
            _commentRepo = commentRepo;
            _tokenExtractor = tokenExtractor;
            _userManagement = userManagement;
           
            RuleFor(x => x.ProductCommentId)
               .NotEmpty()
               .WithMessage(Messages.EmptyField)
               .EntityExist(commentRepo)
              .WithMessage(Messages.NotFound);

            RuleFor(x => x)
                .CustomAsync(Verify);
        }
        private async Task Verify(DeletesCommentCommand command, ValidationContext<DeletesCommentCommand> context, CancellationToken cancellationToken)
        {
            var comment = await _commentRepo.GetByIdAsync(command.ProductCommentId);
            if (comment == null)
                context.AddFailure(nameof(command.ProductCommentId), Messages.NotFound);
            else
            {
                var userId = _tokenExtractor.GetUserId();
                var user = await _userManagement.GetUserData(userId);
                if (user.Role == "SuperVisor" && userId != comment.CreatedBy)
                    context.AddFailure(Messages.EmptyBadRequest);
            }
        }
    }
}
