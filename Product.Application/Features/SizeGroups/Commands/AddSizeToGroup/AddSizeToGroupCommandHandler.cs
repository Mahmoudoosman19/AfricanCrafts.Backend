using Product.Domain.Entities;
namespace Product.Application.Features.SizeGroups.Commands.AddSizeToGroup
{
    public sealed class AddSizeToGroupCommandHandler : ICommandHandler<AddSizeToGroupCommand>
    {
        private readonly IGenericRepository<SizeGroup> _sizeGroupRepository;

        public AddSizeToGroupCommandHandler(
            IGenericRepository<SizeGroup> sizeGroupRepository)
        {
            _sizeGroupRepository = sizeGroupRepository;
        }

        public async Task<ResponseModel> Handle(AddSizeToGroupCommand request, CancellationToken cancellationToken)
        {
            var sizeGroup = await _sizeGroupRepository.GetByIdAsync(request.SizeGroupId);

            var size = Size.Create(
                               request.NameAr,
                               request.NameEn,
                               request.DescriptionAr,
                               request.DescriptionEn,
                               request.SizeGroupId);

            sizeGroup!.AddSize(size);
            await _sizeGroupRepository.SaveChangesAsync(cancellationToken);

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
