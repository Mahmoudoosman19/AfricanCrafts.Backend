using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using MediatR;
using UserManagement.Application.Features.wallet.Command.CreateWallet;
using UserManagement.Application.Specifications.Wallet;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Wallet.Command.AddTransaction
{
    public class AddTransactionHandler(
        IUnitOfWork unitOfWork,
        ISender sender) : ICommandHandler<AddTransactionBatchCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ISender _sender = sender;

        public async Task<ResponseModel<bool>> Handle(AddTransactionBatchCommand request, CancellationToken cancellationToken)
        {
            //var adminPercentage = 0.25;
            if (request.Commands == null || !request.Commands.Any())
                return ResponseModel.Failure<bool>(Messages.NoTransactionsFound);

            var admin = _unitOfWork.Repository<Domain.Entities.User>().Get().FirstOrDefault(x => x.RoleId == 1);
            if (admin == null)
                return ResponseModel.Failure<bool>(Messages.AdminNotFound);

            var systemPercentage = _unitOfWork.Repository<Domain.Entities.MetaData>().Get().FirstOrDefault();
            if (systemPercentage == null)
                return ResponseModel.Failure<bool>(Messages.SystemPercentageNotFound);

            try
            {
                foreach (var command in request.Commands)
                {
                    if (command.AmountOfTheOrder <= 0)
                        return ResponseModel.Failure<bool>(Messages.IncorrectData);

                    var walletResult = await GetOrCreateWalletAsync(command.UserId, cancellationToken);
                    if (!walletResult.IsSuccess)
                        return ResponseModel.Failure<bool>(walletResult.Message);

                    var wallet = walletResult.Data;
                    var currentBalance = wallet.CurrentBalance;

                    if (command.AdjustmentType == AdjustmentType.Subtraction && currentBalance < command.AmountOfTheOrder)
                        return ResponseModel.Failure<bool>(Messages.IncorrectData);

                    var newBalance = command.AdjustmentType == AdjustmentType.Addition
                        ? currentBalance + (command.AmountOfTheOrder * (systemPercentage.ApplicationRate))
                        : currentBalance - (command.AmountOfTheOrder * (systemPercentage.ApplicationRate));

                    wallet.SetCurrentBalance(newBalance);

                    if (command.AdjustmentType == AdjustmentType.Addition)
                        wallet.SetTotalEarnings(newBalance);
                    else
                        wallet.subtractTotalEarnings(newBalance);

                    wallet.SetNumberOfCompletedTransactions();

                    var transaction = new WalletTransaction();
                    transaction.SetOrderId(command.OrderId);
                    transaction.SetWalletId(wallet.Id);
                    transaction.SetPriceOfTheOrder(command.AmountOfTheOrder);
                    transaction.SetBalanceBeforeTheTransaction(currentBalance);
                    transaction.SetBalanceAfterTheTransaction(newBalance);
                    transaction.SetAdjustmentType(command.AdjustmentType);

                    await _unitOfWork.Repository<WalletTransaction>()
                        .AddAsync(transaction, cancellationToken);
                }

                await _unitOfWork.CompleteAsync(cancellationToken);
                return ResponseModel.Success(true);
            }
            catch (Exception ex)
            {
                return ResponseModel.Failure<bool>($"Error processing transactions: {ex.Message}");
            }
        }

        private async Task<ResponseModel<Domain.Entities.Wallet>> GetOrCreateWalletAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var wallet = _unitOfWork.Repository<Domain.Entities.Wallet>()
                .GetEntityWithSpec(new GetWalletByUserIdSpecification(userId));

            if (wallet != null)
                return ResponseModel.Success(wallet);

            var createCommand = new CreateWalletCommand { UserId = userId };
            var createResult = await _sender.Send(createCommand, cancellationToken);

            if (!createResult.IsSuccess)
                return ResponseModel.Failure<Domain.Entities.Wallet>(createResult.Message);

            wallet = _unitOfWork.Repository<Domain.Entities.Wallet>()
                .GetEntityWithSpec(new GetWalletByUserIdSpecification(userId));

            return wallet != null
                ? ResponseModel.Success(wallet)
                : ResponseModel.Failure<Domain.Entities.Wallet>(Messages.NoWalletFound);
        }
    }
}