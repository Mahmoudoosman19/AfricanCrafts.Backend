using Product.Application.Abstractions;
using Product.Domain.Abstraction;

namespace Product.Application.Features.Product.Commands.UpdateProduct.UpdateProductHandlers
{
    internal class UpdateProductDetailsHandler : ResponsibilityHandler<UpdateProductInput>
    {
        private readonly IProductUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductDetailsHandler(IProductUnitOfWork unitOfWork, IMapper mapper)
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
