using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Order.Application.Features.Checkout.Command.CheckoutOrder;
using Order.Application.Features.Order.Command.AdminManageOrderClosing;
using Order.Application.Features.Order.Command.CustomerCancelOrder;
using Order.Application.Features.Order.Command.DashboardCancelOrder;
using Order.Application.Features.Order.Command.RefundOrder;
using Order.Application.Features.Order.Queries.AdminGetListOrder;
using Order.Application.Features.Order.Queries.AdminGetOrderByIdsAndSendToCustomer;
using Order.Application.Features.Order.Queries.AdminGetOrderByIdsAndSendToSippingCompany;
using Order.Application.Features.Order.Queries.AdminGetOrderDelivered;
using Order.Application.Features.Order.Queries.AdminGetOrderReturned;
using Order.Application.Features.Order.Queries.AdminGetShippingReport;
using Order.Application.Features.Order.Queries.DashboardViewOrderDetails;
using Order.Application.Features.Order.Queries.ExportOrdersToExcel;
using Order.Application.Features.Order.Queries.GetBestSeller;
using Order.Application.Features.Order.Queries.GetOrderByProductIdAndCustomerId;
using Order.Application.Features.Order.Queries.GetOrderByStatusId;
using Order.Application.Features.Order.Queries.MonthlyDeliveredOrderStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ApiController
    {
        public OrderController(ISender sender): base(sender) { }

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(CheckoutOrderCommand command)
        {
            var result = await Sender.Send(command);
            return HandleResult(result);
        }
    }
}
