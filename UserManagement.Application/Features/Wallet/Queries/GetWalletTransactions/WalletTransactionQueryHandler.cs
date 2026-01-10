using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using UserManagement.Application.Features.Wallet.Queries.GetAdminWalletTransactions;
using UserManagement.Application.Specifications.Wallet;
using UserManagement.Domain.Abstraction;

namespace UserManagement.Application.Features.wallet.Queries.GetWalletTransactions
{
    internal class WalletTransactionQueryHandler : IQueryHandler<WalletTransactionQuery, List<WalletTransactionQueryResponse>>
    {
        private readonly IUserRepository<WalletTransaction> _walletTransactionRepository;
        private readonly ITokenExtractor _tokenExtractor;
        private readonly IMapper _mapper;
        public WalletTransactionQueryHandler(IUserRepository<WalletTransaction> walletTransactionRepository, ITokenExtractor tokenExtractor, IMapper mapper)
        {
            _walletTransactionRepository = walletTransactionRepository;
            _tokenExtractor = tokenExtractor;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<WalletTransactionQueryResponse>>> Handle(WalletTransactionQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == null && request.UserWalletId == null)
                request.UserId = _tokenExtractor.GetUserId();

            var spec = new GetWalletTransactionsByIdSpecification(request);
            (var userWalletTransaction, int count) = _walletTransactionRepository.GetWithSpec(spec);

            if (!userWalletTransaction.Any())
                return ResponseModel.Failure<List<WalletTransactionQueryResponse>>(Messages.NotFound);


            var mappedUserWalletTransaction = _mapper.Map<List<WalletTransactionQueryResponse>>(userWalletTransaction);
            return ResponseModel.Success(mappedUserWalletTransaction, count);
        }
    }
}
