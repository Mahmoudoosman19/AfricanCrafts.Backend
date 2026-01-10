using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using UserManagement.Domain.Abstraction;

namespace UserManagement.Application.Features.wallet.Command.CreateWallet
{
    internal class CreateWalletCommandHandler : ICommandHandler<CreateWalletCommand>
    {
        private readonly IUserRepository<Domain.Entities.Wallet> _WalletRepository;
        public CreateWalletCommandHandler(IUserRepository<Domain.Entities.Wallet> walletRepository)
        {
            _WalletRepository = walletRepository;
        }
        public async Task<ResponseModel> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var wallet = new Domain.Entities.Wallet();

                wallet.SetUserId(request.UserId);
                wallet.SetCurrentBalance(0);
                wallet.SetTotalEarnings(0);
                await _WalletRepository.AddAsync(wallet);
                await _WalletRepository.SaveChangesAsync();

                return ResponseModel.Success();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
