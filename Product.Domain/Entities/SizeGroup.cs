using Common.Domain.Primitives;

namespace Product.Domain.Entities
{
    public class SizeGroup : Entity<Guid>, IAuditableEntity
    {
        private List<Size> _sizes = [];
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public ICollection<Size> Sizes => _sizes;
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        private List<SizeGroupQuestion> _sizeGroupQuestions = [];

        public ICollection<SizeGroupQuestion> SizeGroupQuestions => _sizeGroupQuestions;

        public void SetName(string nameAr, string nameEn)
        {
            NameAr = nameAr;
            NameEn = nameEn;
        }
        public void AddSize(Size size)
        {
            if (size == null) throw new ArgumentNullException(nameof(size));
            _sizes.Add(size);
        }
        public void AddRangeSize(List<Size> sizes)
        {
            if (sizes == null) throw new ArgumentNullException(nameof(sizes));
            _sizes.AddRange(sizes);
        }

        public void RemoveSize(Size size)
        {
            if (size == null) throw new ArgumentNullException(nameof(size));
            _sizes.Remove(size);
        }

        public void UpdateSizes(List<Size> sizes)
        {
            if (sizes == null) throw new ArgumentNullException(nameof(sizes));
            _sizes.Clear();
            _sizes.AddRange(sizes);
        }

        public void UpdateQuestions(List<SizeGroupQuestion> questions)
        {
            if (questions == null) throw new ArgumentNullException(nameof(questions));
            _sizeGroupQuestions.Clear();
            _sizeGroupQuestions.AddRange(questions);
        }

        public void AddRangeQuestions(List<SizeGroupQuestion> questions)
        {
            if (questions == null) throw new ArgumentNullException(nameof(questions));
            _sizeGroupQuestions.AddRange(questions);
        }
    }
}