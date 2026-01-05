using Common.Domain.Repositories;
using Common.Domain.Shared;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.Wallet;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Seeders
{
    public class AdminWalletSeeder : ISeeder
    {
        private readonly IGenericRepository<Wallet> _WalletRepository;
        private readonly CustomUserManager _userManager;
        public int ExecutionOrder { get; set; } = 6;

        public AdminWalletSeeder(IGenericRepository<Wallet> walletRepository, CustomUserManager userManager)
        {
            _WalletRepository = walletRepository;
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            var user = await _userManager.FindByEmailAsync("admin@admin.com");
            if (user == null)
                return;
            var spec = new GetWalletByUserIdSpecification(user.Id);
            var wallet = _WalletRepository.GetEntityWithSpec(spec);

            if (wallet != null)
                return;
            var vendorWallet = new Wallet();

            vendorWallet.SetUserId(user.Id);
            vendorWallet.SetCurrentBalance(0);
            vendorWallet.SetTotalEarnings(0);
            await _WalletRepository.AddAsync(vendorWallet);
            await _WalletRepository.SaveChangesAsync();

        }
    }
}
