//using Grpc.Net.Client;
//using Microsoft.Extensions.Options;
//using UserManagement.Application.Abstractions;
//using UserManagement.Application.SharedDTOs;
//using UserManagement.Domain.Options;
//using UserManagement.Infrastructure.Protos;


//namespace UserManagement.Service
//{
//    public class OrderUserService : IOrderUserService
//    {
//        private readonly GRPCOptions _grpcOptions;
//        public OrderUserService(IOptions<GRPCOptions> grpcOptions)
//        {
//            _grpcOptions = grpcOptions.Value;
//        }

//        public async Task<List<PendingCancellationOrdersResponseDto>> GetAllPendingCancellationOrders()
//        {

//            var httpHandler = new HttpClientHandler();
//            httpHandler.DefaultProxyCredentials = System.Net.CredentialCache.DefaultCredentials;

//            var channel = GrpcChannel.ForAddress(
//                _grpcOptions.OrderChannelAddress,
//                new GrpcChannelOptions
//                {
//                    HttpHandler = httpHandler
//                });

//            var client = new OrderUserManagement.OrderUserManagementClient(channel);

//            var request = new GetAllPendingCancellationOrdersRequest();
//            var response = await client.GetAllPendingCancellationOrdersAsync(request);

//            if (response.IsSuccess && response.Data != null)
//            {
//                // Map the data into a list of PendingCancellationOrdersResponseDto
//                var userList = response.Data.Select(data => new PendingCancellationOrdersResponseDto
//                {
//                    OrderId = Guid.Parse(data.OrderId),
//                    CustomerId = Guid.Parse(data.CustomerId),
//                    DateOfRequest = DateTime.Parse(data.DateOfRequest),
//                    Status = data.StatusName,
//                    ReturnedReason = data.ReturnedReason
//                }).ToList();

//                return userList;
//            }
//            else
//            {
//                return new List<PendingCancellationOrdersResponseDto>();
//            }
//        }

//    }
//}