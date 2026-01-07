using Bus.Contracts.Models.Order;

namespace Bus.Contracts.Order
{
    public sealed record CalculateProfitShareContract(List<OrderDataModel> Orders);
}
