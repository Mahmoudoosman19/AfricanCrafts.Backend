using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using UserManagement.Application.Specifications.Customer;
using UserManagement.Domain.Abstraction;

namespace UserManagement.Application.Features.Customer.Queries.CheckIfCustomerHasAvatar
{
    internal class CheckIfCustomerHasAvatarQueryHandler : IQueryHandler<CheckIfCustomerHasAvatarQuery, CheckIfCustomerHasAvatarQueryResponse>
    {
        private readonly IUserRepository<Domain.Entities.Customer> _customerRepo;
        private readonly ITokenExtractor _tokenExtractor;
        private readonly IMapper _mapper;
        public CheckIfCustomerHasAvatarQueryHandler(ITokenExtractor tokenExtractor, IUserRepository<Domain.Entities.Customer> customerRepo, IMapper mapper)
        {
            _tokenExtractor = tokenExtractor;
            _customerRepo = customerRepo;
            _mapper = mapper;
        }
        public async Task<ResponseModel<CheckIfCustomerHasAvatarQueryResponse>> Handle(CheckIfCustomerHasAvatarQuery request, CancellationToken cancellationToken)
        {
            if (request.userId == null)
                request.userId = _tokenExtractor.GetUserId();

            var userAvatarSpec = new CheckIfCustomerHasAvatarSpecification(request.userId.Value);
            var userAvatar = _customerRepo.GetEntityWithSpec(userAvatarSpec);
            if (userAvatar == null)
                return ResponseModel.Failure<CheckIfCustomerHasAvatarQueryResponse>(Messages.NotFound);

            var mapped = _mapper.Map<CheckIfCustomerHasAvatarQueryResponse>(userAvatar);
            var CheckIfCustomerHasAvatarQueryResponse = new CheckIfCustomerHasAvatarQueryResponse()
            {
                CustomerHasAvatar = true,
                CustomerBaseAvatarUrl = $"https://ik.imagekit.io/cfqg4uunc/tr:n-ik_ml_thumbnail/avatars/{userAvatar.BaseAvatarId.ToString().ToLower()}",
                CustomerCustomizedAvatarUrl = $"https://ik.imagekit.io/cfqg4uunc/tr:n-ik_ml_thumbnail/customizedavatars/{userAvatar?.UserId.ToString().ToLower()}/{userAvatar?.CustomizedAvatarId?.ToString().ToLower()}.png"
            };
            return ResponseModel.Success(CheckIfCustomerHasAvatarQueryResponse);
        }
    }
}
