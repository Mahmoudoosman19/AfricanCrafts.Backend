using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MediatR;
using UserManagement.Application.Features.Wallet.Command.AddTransaction;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Wallet.Command.DistributeProfitShare
{
    internal class DistributeProfitShareHandler(
        IGenericRepository<Domain.Entities.MetaData> metaRepo,
        IGenericRepository<Domain.Entities.User> userRepo,
        ISender sender) : IQueryHandler<DistributeProfitShareCommand, bool>
    {
        private readonly IGenericRepository<Domain.Entities.MetaData> _metaRepo = metaRepo;
        private readonly IGenericRepository<Domain.Entities.User> _userRepo = userRepo;
        private readonly ISender _sender = sender;

        public async Task<ResponseModel<bool>> Handle(
            DistributeProfitShareCommand request,
            CancellationToken cancellationToken)
        {
            var systemPercentage = _metaRepo.Get().FirstOrDefault();
            if (systemPercentage == null)
                return ResponseModel.Failure<bool>(Messages.SystemPercentageNotFound);

            var admin = _userRepo.Get().FirstOrDefault(x => x.RoleId == 1);
            if (admin == null)
                return ResponseModel.Failure<bool>(Messages.AdminNotFound);

            try
            {
                var transactionCommands = new List<AddTransactionCommand>();

                foreach (var command in request.Commands)
                {
                    var adminProfitShare = command.TotalOrderPrice * (systemPercentage.ApplicationRate / 100);
                    var vendorProfitShare = command.TotalOrderPrice - adminProfitShare;

                    transactionCommands.Add(new AddTransactionCommand
                    {
                        OrderId = command.OrderId,
                        UserId = admin.Id,
                        AmountOfTheOrder = adminProfitShare,
                        AdjustmentType = AdjustmentType.Addition
                    });

                    transactionCommands.Add(new AddTransactionCommand
                    {
                        OrderId = command.OrderId,
                        UserId = command.VendorId,
                        AmountOfTheOrder = vendorProfitShare,
                        AdjustmentType = AdjustmentType.Addition
                    });
                }

                var batchCommand = new AddTransactionBatchCommand { Commands = transactionCommands };
                var result = await _sender.Send(batchCommand, cancellationToken);

                return result.IsSuccess
                    ? ResponseModel.Success(true)
                    : ResponseModel.Failure<bool>(result.Message);
            }
            catch (Exception ex)
            {
                return ResponseModel.Failure<bool>(ex.Message);
            }
        }
    }
}