
using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.User.Commands.CalculateUserPoints
{
    public class CalculateUserPointsCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public int Points { get; set; }
    }
}
