using Common.Domain.Repositories;
using Order.Application.Abstraction;
using Order.Domain.Entities;
using Order.Domain.Enum;

namespace Order.Infrastructure.Seeder
{
    public class PaymentMethodSeeder : ISeeder
    {
        private readonly IGenericRepository<PaymentMethod> _paymentMethodRepo;

        public PaymentMethodSeeder(IGenericRepository<PaymentMethod> paymentMethodRepo)
        {
            _paymentMethodRepo = paymentMethodRepo;
        }

        public int ExecutionOrder { get; set; } = 1;

        public async Task SeedAsync()
        {
            try
            {
                var seedData = GetSeedData();

                var existingPaymentMethodIds = _paymentMethodRepo
                    .Get()
                    .Select(x => x.Id)
                    .ToList();

                var newPaymentMethods = seedData
                    .Where(sd => !existingPaymentMethodIds.Contains(sd.Id))
                    .ToList();

                if (newPaymentMethods.Any())
                {
                    await _paymentMethodRepo.AddRangeAsync(newPaymentMethods);
                    await _paymentMethodRepo.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding PaymentMethods: {ex.Message}");
            }
        }

        private static IReadOnlyList<PaymentMethod> GetSeedData()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod((long)PaymentMethodEnum.VisaCard, "بطاقة التأشيرة", PaymentMethodEnum.VisaCard.ToString()),
                new PaymentMethod((long)PaymentMethodEnum.Cash, "نقدي", PaymentMethodEnum.Cash.ToString())
            };
        }
    }

}
