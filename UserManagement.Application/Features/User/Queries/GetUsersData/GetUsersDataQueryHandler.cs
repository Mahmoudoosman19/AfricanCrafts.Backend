using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.Specifications.User;
using UserManagement.Domain.Abstraction;

namespace UserManagement.Application.Features.User.Queries.GetUsersData
{
    public class GetUsersDataQueryHandler : IQueryHandler<GetUsersDataQuery, List<GetUsersDataQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository<Domain.Entities.User> _userRepo;

        public GetUsersDataQueryHandler(IMapper mapper, IUserRepository<Domain.Entities.User> userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }
        public async Task<ResponseModel<List<GetUsersDataQueryResponse>>> Handle(GetUsersDataQuery request, CancellationToken cancellationToken)
        {
            var result = _userRepo.GetWithSpec(new GetUsersDataSpecifications(request)).data.ToList();

            var response = _mapper.Map<List<GetUsersDataQueryResponse>>(result);

            return ResponseModel.Success(response);
        }
    }
}
