namespace Product.Application.Abstractions
{
    public  interface IOrderStatus
    {
        Task<bool> GetOrderStatus(Guid ProductId ,Guid CustomerId);
    }
}
