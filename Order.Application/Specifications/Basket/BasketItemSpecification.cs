using Common.Domain.Specification;
using Order.Application.Features.Basket.Command.RemoveItemFromBasket;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Specifications.Basket
{
    public class BasketItemSpecification : Specification<BasketItem>
    {
        public BasketItemSpecification(Guid productId)
        {
            AddCriteria(x => x.ProductId == productId);
        }
    }
}
