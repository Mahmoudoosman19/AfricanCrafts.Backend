using Mapster;
using UserManagement.Application.Features.Wallet.Queries.GetAdminWalletTransactions;

namespace UserManagement.Application.Features.Wallet.MappingConfig
{
    internal class GetAdminWalletTransactionsMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WalletTransaction, GetAdminWalletTransactionsQueryResponse>()
                .Map(dest => dest.VendorNameAr, src => src.Wallet.User.FullNameAr)
                .Map(dest => dest.VendorNameEn, src => src.Wallet.User.FullNameEn);
        }
    }
}
