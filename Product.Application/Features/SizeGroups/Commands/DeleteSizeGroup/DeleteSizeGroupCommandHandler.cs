using Product.Domain.Entities;

namespace Product.Application.Features.TemplateSizes.Commands.DeleteSizeGroup
{
    internal class DeleteSizeGroupCommandHandler : ICommandHandler<DeleteSizeGroupCommand>
    {
        private readonly IUnitOfWork _unitOfWork;


        public DeleteSizeGroupCommandHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(DeleteSizeGroupCommand request, CancellationToken cancellationToken)
        {
            var sizeGroup = await _unitOfWork.Repository<SizeGroup>().GetByIdAsync(request.Id, cancellationToken);
            _unitOfWork.Repository<SizeGroup>().Delete(sizeGroup!);

            await _unitOfWork.CompleteAsync(cancellationToken);

            return ResponseModel
                .Success(Messages.SuccessfulOperation);
        }
    }
}
