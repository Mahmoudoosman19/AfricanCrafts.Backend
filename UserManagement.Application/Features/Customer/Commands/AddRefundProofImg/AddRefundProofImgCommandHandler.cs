using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Enums;
using UserManagement.Application.Specifications.RefundProof;

namespace UserManagement.Application.Features.Customer.Commands.AddRefundProofImg
{
    internal class AddRefundProofImgCommandHandler : ICommandHandler<AddRefundProofImgCommand, string>
    {
        private readonly IGenericRepository<Domain.Entities.RefundProof> _refundProofRepo;
        private readonly IGenericRepository<Domain.Entities.WalletTransaction> _walletTransaction;

        private readonly IImageKitService _imageKitService;
        public AddRefundProofImgCommandHandler(IGenericRepository<Domain.Entities.RefundProof> refundProofRepo, IImageKitService imageKitService , IGenericRepository<Domain.Entities.WalletTransaction> walletTransaction)
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
