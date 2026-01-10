using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.Specifications.Wallet;
using UserManagement.Domain.Abstraction;

namespace UserManagement.Application.Features.Wallet.Queries.GetAdminWalletTransactions
{
    internal class GetAdminWalletTransactionsQueryHandler : IQueryHandler<GetAdminWalletTransactionsQuery, IEnumerable<GetAdminWalletTransactionsQueryResponse>>
    {
        private readonly IUserRepository<WalletTransaction> _walletTransactionRepo;
        private readonly IMapper _mapper;
        public GetAdminWalletTransactionsQueryHandler(IUserRepository<WalletTransaction> walletTransactionRepo, IMapper mapper)
        {
            _walletTransactionRepo = walletTransactionRepo;
            _mapper = mapper;
        }
        public async Task<ResponseModel<IEnumerable<GetAdminWalletTransactionsQueryResponse>>> Handle(GetAdminWalletTransactionsQuery request, CancellationToken cancellationToken)
        {
            var walletTransactionSpec = new GetAdminWalletTransactionsSpecification(request);
            (var walletTransactionData, int count) = _walletTransactionRepo.GetWithSpec(walletTransactionSpec);

            if (!walletTransactionData.Any())
                return ResponseModel.Failure<IEnumerable<GetAdminWalletTransactionsQueryResponse>>(Messages.NotFound);

            var mappedWalletTransactions = _mapper.Map<IEnumerable<GetAdminWalletTransactionsQueryResponse>>(walletTransactionData);
            return ResponseModel.Success(mappedWalletTransactions, count);
        }
    }
}
