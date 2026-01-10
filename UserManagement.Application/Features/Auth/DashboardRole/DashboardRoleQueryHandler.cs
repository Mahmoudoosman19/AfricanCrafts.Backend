using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using UserManagement.Application.Specifications.User;
using UserManagement.Domain.Abstraction;

namespace UserManagement.Application.Features.Auth.DashboardRole
{
    public class DashboardRoleQueryHandler : IQueryHandler<DashboardRoleQuery, DashboardRoleResponse>
    {
        private readonly ITokenExtractor _tokenExtractor;
        private readonly IMapper _mapper;
        private readonly IUserUnitOfWork _unitOfWork;

        public DashboardRoleQueryHandler(ITokenExtractor tokenExtractor, IMapper mapper, IUserUnitOfWork unitOfWork)
        {
            _tokenExtractor = tokenExtractor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<DashboardRoleResponse>> Handle(DashboardRoleQuery request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();
            if (string.IsNullOrEmpty(userId.ToString()))
            {
                  return ResponseModel.Failure<DashboardRoleResponse>(Messages.UserNotFound);
            }
            var user =  _unitOfWork.Repository<UserManagement.Domain.Entities.User>().GetEntityWithSpec(new DashboardGetUserByIdWithRoleSpecification(userId));
            if (user == null)
            {
                 return ResponseModel.Failure<DashboardRoleResponse>(Messages.UserNotFound);
            }
            var response = _mapper.Map<DashboardRoleResponse>(user);
            return ResponseModel<DashboardRoleResponse>.Success(response);
        }
    }

}
