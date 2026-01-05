using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MapsterMapper;
using MediatR;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Features.User.Queries.GetUserData;

namespace UserManagement.Application.Features.OrderUser.Queries.GetPendingCancellationOrders
{
    internal class GetPendingCancellationOrdersHandler : IQueryHandler<GetPendingCancellationOrdersQuery, List<GetPendingCancellationOrdersResponse>>
    {
        private readonly IOrderUserService _orderUserService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetPendingCancellationOrdersHandler(IOrderUserService orderUserService, IMapper mapper, IMediator mediator)
        {
            _orderUserService = orderUserService;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ResponseModel<List<GetPendingCancellationOrdersResponse>>> Handle(GetPendingCancellationOrdersQuery request, CancellationToken cancellationToken)
        {
            // Fetch all pending orders
            var orders = await _orderUserService.GetAllPendingCancellationOrders();

           

            var mappedOrders = orders.Select(order => new GetPendingCancellationOrdersResponse
            {
                OrderId = order.OrderId,  // Example property
                StatusName = order.Status,
                CustomerId=order.CustomerId,
                DateOfRequest=order.DateOfRequest// Example property
            }).ToList();


            // Iterate through orders and fetch user data for each one
            foreach (var order in mappedOrders)
            {
                try
                {
                    var userQuery = new GetUserDataQuery { Id = order.CustomerId };
                    var userData = await _mediator.Send(userQuery, cancellationToken);

                    // Safely assign CustomerName and CustomerPhoneNumber with null handling
                    order.CustomerName = string.IsNullOrWhiteSpace(userData.Data?.FullNameEn)
                        ? "Name Not Found"
                        : userData.Data.FullNameEn;

                    order.CustomerPhoneNumber = string.IsNullOrWhiteSpace(userData.Data?.PhoneNumber)
                        ? "Phone Number Not Found"
                        : userData.Data.PhoneNumber;
                }
                catch (Exception ex)
                {
                    // Log the error and assign default values
                    Console.WriteLine($"Error fetching data for CustomerId {order.CustomerId}: {ex.Message}");
                    order.CustomerName = "Name Not Found";
                    order.CustomerPhoneNumber = "Phone Number Not Found";
                }
            }

          var pagedOrders = mappedOrders
            .Skip((request.PageIndex - 1) * request.PageSize)
             .Take(request.PageSize)
                .ToList();

            // Return the enhanced response
            return ResponseModel.Success(pagedOrders, pagedOrders.Count);
        }
    }
}
