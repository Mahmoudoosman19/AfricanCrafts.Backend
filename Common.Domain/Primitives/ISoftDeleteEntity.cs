namespace Common.Domain.Primitives
{
    public interface ISoftDeleteEntity
    {
        public bool IsDeleted { get; }
        public DateTime? DeletedAt { get; }
        public DateTime? RestoredAt { get; }
    }
}
