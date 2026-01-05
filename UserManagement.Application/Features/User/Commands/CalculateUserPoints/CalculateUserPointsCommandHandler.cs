using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;

namespace UserManagement.Application.Features.User.Commands.CalculateUserPoints
{
    internal class CalculateUserPointsCommandHandler(IGenericRepository<Domain.Entities.User> repo) : ICommandHandler<CalculateUserPointsCommand>
    {
        private readonly IGenericRepository<Domain.Entities.User> _repo = repo;

        public async Task<ResponseModel> Handle(CalculateUserPointsCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByIdAsync(request.CustomerId);

            if(user == null)
                return ResponseModel.Failure(Messages.UserNotFound);

            user.AddPoints(request.Points);
            await _repo.SaveChangesAsync();

            return ResponseModel.Success();
        }
    }
}
