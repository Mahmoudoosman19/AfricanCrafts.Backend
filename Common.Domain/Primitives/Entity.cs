namespace Common.Domain.Primitives;
public abstract class Entity<TKey> : ValueObject where TKey : IEquatable<TKey>
{
    protected Entity(TKey id) => Id = id;
    protected Entity()
    {
    }
    public TKey Id { get;  init; }
    public override int GetHashCode() => Id.GetHashCode() * 41;
}
