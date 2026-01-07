namespace Product.Application.Abstractions
{
    public interface IResponsibilityHandler<T>
    {
        IResponsibilityHandler<T> SetNextHandler(IResponsibilityHandler<T> nextHandler);
        Task Handle(T request);
    }
}
