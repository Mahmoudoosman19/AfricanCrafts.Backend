using Common.Domain.Primitives;
using System.Security.Cryptography;
using System.Text;

namespace Product.Domain.Entities
{
    public class Coupon : Entity<Guid>, IAuditableEntity
    {
        private List<CouponProducts> _productCoupons = [];
        public string Code { get; private set; } = null!;
        public int UserCount { get; private set; }
        public double DiscountPercentage { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public Guid UserId { get; private set; }
        public ICollection<CouponProducts> Items => _productCoupons;

        public bool IsManuallyDeactivated { get; private set; }

        public void SetCode(string code)
        {
            Code = code;
        }

        public void SetUserCount(int userCount)
        {
            UserCount = userCount;
        }

        public void SetDiscountPercentage(double discountPercentage)
        {
            DiscountPercentage = discountPercentage;
        }

        public void SetActivation(bool isActive)
        {
            IsActive = isActive;
        }

        public void SetIsManuallyDeactivated(bool isManuallyDeactivated)
        {
            IsManuallyDeactivated = isManuallyDeactivated;
        }

        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }

        public void SetExpireDate(DateTime expireDate)
        {
            ExpireDate = expireDate;
        }

        public void SetUser(Guid userId)
        {
            UserId = userId;
        }

        public void GenerateCode(int length = 8)
        {
            const string validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(length);
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] buffer = new byte[length * 2];
                while (result.Length < length)
                {
                    rng.GetBytes(buffer);
                    for (int i = 0; i < buffer.Length && result.Length < length; i++)
                    {
                        var rnd = buffer[i] % validCharacters.Length;
                        result.Append(validCharacters[rnd]);
                    }
                }
            }
            Code = result.ToString();
        }
        public void AddRangeProductCoupons(List<CouponProducts> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            _productCoupons.AddRange(items);
        }
    }
}
