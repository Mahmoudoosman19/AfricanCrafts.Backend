using Common.Domain.Primitives;

namespace Order.Domain.Entities
{
    public class PaymentMethod : Entity<long>
    {
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public PaymentMethod() { }
        public PaymentMethod(long id, string nameAr, string nameEn)
        {
            Id = id;
            NameAr = nameAr;
            NameEn = nameEn;
        }
    }
}
