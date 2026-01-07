using IdentityHelper.Abstraction;
using Product.Domain.Entities;

namespace Product.Application.Features.Product.Commands.SupervisorRejectProduct;

public class SupervisorRejectProductCommandHandler : ICommandHandler<SupervisorRejectProductCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ITokenExtractor _tokenExtractor;

    public SupervisorRejectProductCommandHandler(IUnitOfWork uow, IMapper mapper, ITokenExtractor tokenExtractor)
    {
        _uow = uow;
        _mapper = mapper;
        _tokenExtractor = tokenExtractor;
    }

    public async Task<ResponseModel> Handle(SupervisorRejectProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _uow.Repository<Domain.Entities.Product>().GetByIdAsync(request.Id, cancellationToken);

        product!.SetActivation(false);
        product.SetStatus(Domain.Enums.ProductStatus.Rejected);
        var userId = _tokenExtractor.GetUserId();
        var comment = new ProductComment(request.Comment, userId);
        product.AddComment(comment);

        await _uow.CompleteAsync(cancellationToken);

        return ResponseModel.Success();
    }
}