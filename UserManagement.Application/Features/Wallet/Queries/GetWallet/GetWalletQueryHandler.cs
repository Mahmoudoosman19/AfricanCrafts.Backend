using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using UserManagement.Application.Specifications.Wallet;
using UserManagement.Domain.Abstraction;

namespace UserManagement.Application.Features.wallet.Queries.GetWallet
{
    internal class GetWalletQueryHandler : IQueryHandler<GetWalletQuery, Domain.Entities.Wallet>
    {
        private readonly IUserRepository<Domain.Entities.Wallet> _userWalletRepository;
        private readonly ITokenExtractor _tokenExtractor;
        private readonly IMapper _mapper;
        public GetWalletQueryHandler(IUserRepository<Domain.Entities.Wallet> userWalletRepository, ITokenExtractor tokenExtractor, IMapper mapper)
        {
            _userWalletRepository = userWalletRepository;
            _tokenExtractor = tokenExtractor;
            _mapper = mapper;
        }
        public async Task<ResponseModel<Domain.Entities.Wallet>> Handle(GetWalletQuery request, CancellationToken cancellationToken)
        {

            var userId = _tokenExtractor.GetUserId();
            var spec = new GetWalletByUserIdSpecification(userId);
            var userWallet = _userWalletRepository.GetEntityWithSpec(spec);
            if (userWallet == null)
                return ResponseModel.Failure<Domain.Entities.Wallet>(Messages.NotFound);

            return ResponseModel.Success(userWallet);
        }
    }
}
