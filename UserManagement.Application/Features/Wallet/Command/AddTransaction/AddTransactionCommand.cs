using Common.Application.Abstractions.Messaging;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Wallet.Command.AddTransaction
{
    public class AddTransactionBatchCommand : ICommand<bool>
    {
        public List<AddTransactionCommand> Commands { get; set; }
    }

    public class AddTransactionCommand
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public decimal AmountOfTheOrder { get; set; }
        public AdjustmentType AdjustmentType { get; set; }
    }
}
