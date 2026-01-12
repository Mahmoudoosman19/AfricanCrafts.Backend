using Common.Domain.Repositories;
using Order.Application.Abstraction;
using Order.Domain.Entities;
using Order.Domain.Enum;

namespace Order.Infrastructure.Seeder
{
    public class PaymentStatusSeeder : ISeeder
    {
        private readonly IGenericRepository<PaymentStatus> _paymentStatusRepo;

        public PaymentStatusSeeder(IGenericRepository<PaymentStatus> paymentStatusRepo)
        {
            _paymentStatusRepo = paymentStatusRepo;
        }

        public int ExecutionOrder { get; set; } = 1;

        public async Task SeedAsync()
        {
            try
            {
                var seedData = GetSeedData();

                var existingPaymentStatusIds = _paymentStatusRepo
                    .Get()
                    .Select(x => x.Id)
                    .ToList();

                var newPaymentStatuses = seedData
                    .Where(sd => !existingPaymentStatusIds.Contains(sd.Id))
                    .ToList();

                if (newPaymentStatuses.Any())
                {
                    await _paymentStatusRepo.AddRangeAsync(newPaymentStatuses);
                    await _paymentStatusRepo.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding PaymentStatus: {ex.Message}");
            }
        }

        private static IReadOnlyList<PaymentStatus> GetSeedData()
        {
            return new List<PaymentStatus>
            {
                new PaymentStatus((long)PaymentStatusEnum.Paid, "مدفوع", PaymentStatusEnum.Paid.ToString()),
                new PaymentStatus((long)PaymentStatusEnum.Unpaid, "غير مدفوع", PaymentStatusEnum.Unpaid.ToString())
            };
        }
    }

}
