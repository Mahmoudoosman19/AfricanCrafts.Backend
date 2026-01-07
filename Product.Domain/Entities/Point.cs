using Common.Domain.Primitives;

namespace Product.Domain.Entities
{
    public class Point : Entity<Guid>, IAuditableEntity
    {
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public decimal Money { get; private set; }
        public int RewardedPoints { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public void SetName(string nameAr, string nameEn)
        {
            NameAr = nameAr;
            NameEn = nameEn;
        }

        public void SetMoney(decimal money)
        {
            Money = money;
        }

        public void SetRewardedPoint(int rewardedPoint)
        {
            RewardedPoints = rewardedPoint;
        }
    }
}
