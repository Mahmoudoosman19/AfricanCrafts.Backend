using Common.Domain.Primitives;

namespace Product.Domain.Entities
{
    public class Color : Entity<Guid>
    {
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public string Code { get; private set; } = null!;
        public void SetName(string nameAr, string nameEn)
        {
            NameAr = nameAr;
            NameEn = nameEn;
        }
    }
}
