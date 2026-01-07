using Product.Application.Abstractions;

namespace Product.Application.Features.Product.Commands.UpdateProduct.UpdateProductHandlers
{
    internal class UpdateProductDetailsHandler : ResponsibilityHandler<UpdateProductInput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task Handle(UpdateProductInput input)
        {
            _mapper.Map(input.Request, input.DBProduct);
            input.DBProduct.SetActivation(false);
            await CallNext(input);
        }
    }
}
