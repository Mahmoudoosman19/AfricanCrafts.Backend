using Common.Application.Extensions.Mapster;
using Product.Domain.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.SizeGroups.Commands.UpdateSizeGroup
{
    internal class UpdateSizeGroupCommandHandler : ICommandHandler<UpdateSizeGroupCommand>
    {
        private readonly IProductUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public UpdateSizeGroupCommandHandler(
            IProductUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> Handle(UpdateSizeGroupCommand request, CancellationToken cancellationToken)
        {
            var sizeGroup = _unitOfWork.Repository<SizeGroup>()
                .GetEntityWithSpec(new GetSizeGroupWithSizeSpecification(request.Id));

            UpdateSizes(request, sizeGroup);

            UpdateQuestions(request, sizeGroup);

            await _unitOfWork.CompleteAsync(cancellationToken);

            return ResponseModel.Success();
        }

        private void UpdateSizes(UpdateSizeGroupCommand request, SizeGroup sizeGroup)
        {
            var oldSizes = sizeGroup!.Sizes.ToList();

            var edited = sizeGroup.Sizes.Where(x => request.EditedSizes!.Any(y => y.Id == x.Id)).ToList();
            var newList = _mapper.Map<List<Size>>(request.EditedSizes!.Where(x => x.Id == Guid.Empty).ToList());

            _mapper.Map(request, sizeGroup);
            sizeGroup.UpdateSizes(edited);

            request.EditedSizes!.UpdateNestedListObject<UpdateSizeDto, Size, Guid>(edited, _mapper);

            sizeGroup.AddRangeSize(newList);

            var removedSizes = oldSizes.Except(edited).ToList();
            _unitOfWork.Repository<Size>().DeleteRange(removedSizes);
        }

        private void UpdateQuestions(UpdateSizeGroupCommand request, SizeGroup sizeGroup)
        {
            var oldQuestions = sizeGroup!.SizeGroupQuestions.ToList();

            var edited = sizeGroup.SizeGroupQuestions.Where(x => request.EditedQuestions!.Any(y => y.Id == x.Id)).ToList();
            var newList = _mapper.Map<List<SizeGroupQuestion>>(request.EditedQuestions!.Where(x => x.Id == Guid.Empty).ToList());

            _mapper.Map(request, sizeGroup);
            sizeGroup.UpdateQuestions(edited);

            request.EditedQuestions!.UpdateNestedListObject<UpdateSizeGroupQuestionDto, SizeGroupQuestion, Guid>(edited, _mapper);

            sizeGroup.AddRangeQuestions(newList);

            var removedQuestions = oldQuestions.Except(edited).ToList();
            _unitOfWork.Repository<SizeGroupQuestion>().DeleteRange(removedQuestions);
        }
    }
}