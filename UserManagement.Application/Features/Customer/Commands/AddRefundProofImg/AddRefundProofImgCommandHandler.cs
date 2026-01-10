using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Enums;
using UserManagement.Application.Specifications.RefundProof;
using UserManagement.Domain.Abstraction;

namespace UserManagement.Application.Features.Customer.Commands.AddRefundProofImg
{
    internal class AddRefundProofImgCommandHandler : ICommandHandler<AddRefundProofImgCommand, string>
    {
        private readonly IUserRepository<Domain.Entities.RefundProof> _refundProofRepo;
        private readonly IUserRepository<Domain.Entities.WalletTransaction> _walletTransaction;

        private readonly IImageKitService _imageKitService;
        public AddRefundProofImgCommandHandler(IUserRepository<Domain.Entities.RefundProof> refundProofRepo, IImageKitService imageKitService , IUserRepository<Domain.Entities.WalletTransaction> walletTransaction)
        {
            _refundProofRepo = refundProofRepo;
            _imageKitService = imageKitService;
            _walletTransaction = walletTransaction;
        }

        public async Task<ResponseModel<string>> Handle(AddRefundProofImgCommand request, CancellationToken cancellationToken)
        {

            var walletTransaction = _walletTransaction.GetEntityWithSpec(new GetTransactionByOrderIdSpecification(request.OrderId));
            if (walletTransaction == null)
                return ResponseModel.Failure<string>(Messages.NotFound);

            try
            {
               var result = await _imageKitService.UploadFileAsync(request.RefundProofImgUrl, FileType.RefundProof);
                var refundProofEntity = new Domain.Entities.RefundProof();
                refundProofEntity.SetRefundProofImgId(result.FileId);
               
                refundProofEntity.SetWalletTransactionId(walletTransaction.Id);

                refundProofEntity.SetRefundProofImgUrl(result.Name);

                await _refundProofRepo.AddAsync(refundProofEntity);
                await _refundProofRepo.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return ResponseModel.Failure<string>($"File upload failed: {ex.Message}");
            }

            
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
