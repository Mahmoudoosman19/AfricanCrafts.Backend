using Product.Application.Features.Favorite.Command.ToggelFavorit;
using Product.Domain.Abstraction;

namespace Product.Application.Features.Favorite.Command.ToggelFavorite
{
    public class ToggelFavoriteCommandValidator: AbstractValidator<ToggelFavoriteCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepo;
        public ToggelFavoriteCommandValidator(
           IProductRepository<Domain.Entities.Product> productRepo
          )
        {
            _productRepo = productRepo;

            RuleFor(x => x.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EntityExist(productRepo).WithMessage(Messages.NotFound)
                .WithMessage(Messages.IncorrectData);
        }
       
    }
}
