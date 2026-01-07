namespace Product.Application.Features.Product.Commands.SupervisorDeletesComment
{
    public class DeletesCommentCommand :ICommand
    {
        public Guid ProductCommentId { get; set; }
    }
}
