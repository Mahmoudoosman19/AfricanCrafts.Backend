using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.wallet.Command.CreateWallet
{
    public class CreateWalletCommand : ICommand
    {
        public Guid UserId { get; set; }
    }
}
