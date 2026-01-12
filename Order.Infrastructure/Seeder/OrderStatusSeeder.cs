using Common.Domain.Repositories;
using Order.Application.Abstraction;
using Order.Domain.Entities;
using Order.Domain.Enum;

namespace Order.Infrastructure.Seeder
{
    public class OrderStatusSeeder : ISeeder
    {
        private readonly IGenericRepository<OrderStatus> _orderStatusRepo;

        public OrderStatusSeeder(IGenericRepository<OrderStatus> orderStatusRepo)
        {
            _orderStatusRepo = orderStatusRepo;
        }

        public int ExecutionOrder { get; set; } = 1;

        public async Task SeedAsync()
        {
            try
            {
                var seedData = GetSeedData();

                var existingOrderStatusIds = _orderStatusRepo
                    .Get()
                    .Select(x => x.Id)
                    .ToList();

                var newOrderStatuses = seedData
                    .Where(sd => !existingOrderStatusIds.Contains(sd.Id))
                    .ToList();

                if (newOrderStatuses.Any())
                {
                    await _orderStatusRepo.AddRangeAsync(newOrderStatuses);
                    await _orderStatusRepo.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding OrderStatus: {ex.Message}");
            }

        }

        private static IReadOnlyList<OrderStatus> GetSeedData()
        {
            return new List<OrderStatus>
            {
                new OrderStatus((long)OrderStatusEnum.Initiated, "بدأت", OrderStatusEnum.Initiated.ToString()),
                new OrderStatus((long)OrderStatusEnum.Shipped, "تم الشحن", OrderStatusEnum.Shipped.ToString()),
                new OrderStatus((long)OrderStatusEnum.Delivered, "تم التوصيل", OrderStatusEnum.Delivered.ToString()),
                new OrderStatus((long)OrderStatusEnum.Canceled, "ملغي", OrderStatusEnum.Canceled.ToString()),
                new OrderStatus((long)OrderStatusEnum.Returned, "مرتجع", OrderStatusEnum.Returned.ToString()),
               new OrderStatus((long)OrderStatusEnum.CustomerReceived, "تاكيد استلام الاوردر", OrderStatusEnum.CustomerReceived.ToString()),
               new OrderStatus((long)OrderStatusEnum.Pending, "معلق", OrderStatusEnum.Pending.ToString()),
               new OrderStatus((long)OrderStatusEnum.Refunded, "تم الاسترداد", OrderStatusEnum.Refunded.ToString())


            };
        }
    }

}
