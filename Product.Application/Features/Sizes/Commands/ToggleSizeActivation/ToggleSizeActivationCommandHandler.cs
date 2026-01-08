
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Sizes.Commands.ToggleSizeStop
{
    internal class ToggleSizeActivationCommandHandler : ICommandHandler<ToggleSizeActivationCommand>
    {
        private readonly IProductRepository<Size> _sizeRepo;

        public ToggleSizeActivationCommandHandler(IProductRepository<Size> sizeRepo)
        {
            _sizeRepo = sizeRepo;
        }
        public async Task<ResponseModel> Handle(ToggleSizeActivationCommand request, CancellationToken cancellationToken)
        {

            var Size = await _sizeRepo.GetByIdAsync(request.SizeId, cancellationToken);

            Size!.SetIsActive(!Size.IsActive);

            _sizeRepo.Update(Size);

            await _sizeRepo.SaveChangesAsync(cancellationToken);


            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
