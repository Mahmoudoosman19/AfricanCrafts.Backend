using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using Order.Application.Abstraction;
using Order.Application.Features.Basket.Query.GetBasketQuery;
using Order.Domain.Abstraction;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Basket.Command.UpdateBasket
{
    internal class UpdateBasketCommandHandler : ICommandHandler<UpdateBasketCommand, GetBasketQueryResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ITokenExtractor _token;

        public UpdateBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper,
            IProductService productService,
            ITokenExtractor token)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _productService = productService;
            _token = token;
        }

        public async Task<ResponseModel<GetBasketQueryResponse>> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var basketId = _token.IsUserAuthenticated()
                         ? _token.GetUserId()
                         : (request.CustomerId ?? Guid.NewGuid());

            var basket = await _basketRepository.GetBasketAsync(basketId)
                             ?? new CustomerBasket(basketId);

            var allProductIds = request.BasketItems.Select(i => i.ProductId).Distinct().ToList();

            var productsData = await _productService.GetProductsBulkAsync(allProductIds, cancellationToken);

            foreach (var itemRequest in request.BasketItems)
            {
                var productDto = productsData.FirstOrDefault(p => p.Id == itemRequest.ProductId);

                if (productDto == null) continue;

                var selectedExt = productDto.Extensions.FirstOrDefault(x => x.Id == itemRequest.ProductExtensionId);

                if (selectedExt == null) continue;

                basket.AddItem(
                    productDto.Id,
                    selectedExt.Id,
                    productDto.NameAr,
                    productDto.NameEn,
                    productDto.Price + selectedExt.Fees,
                    itemRequest.Quantity,
                    selectedExt.ColorCode,
                    selectedExt.Size?.NameEn ?? "");
            }

            var result = await _basketRepository.UpdateBasketAsync(basket);

            var response = _mapper.Map<GetBasketQueryResponse>(result);
            return ResponseModel.Success(response);
        }
    }
}
