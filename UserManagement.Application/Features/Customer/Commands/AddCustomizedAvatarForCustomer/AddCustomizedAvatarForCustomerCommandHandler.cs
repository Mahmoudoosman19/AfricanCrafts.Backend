using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using ImageKitFileManager.Abstractions;
using ImageKitFileManager.Enums;
using UserManagement.Domain.Abstraction;

namespace UserManagement.Application.Features.Customer.Commands.AddCustomizedAvatarForCustomer
{
    internal class AddCustomizedRefundProofForCustomerCommandHandler : ICommandHandler<AddCustomizedAvatarForCustomerCommand, string>
    {
        private readonly IUserRepository<Domain.Entities.Customer> _customerRepo;
        private readonly ITokenExtractor _tokenExtractor;
        private readonly IImageKitService _imageKitService;
        public AddCustomizedRefundProofForCustomerCommandHandler(IUserRepository<Domain.Entities.Customer> customerRepo, ITokenExtractor tokenExtractor, IImageKitService imageKitService)
        {
            _customerRepo = customerRepo;
            _tokenExtractor = tokenExtractor;
            _imageKitService = imageKitService;
        }
        public async Task<ResponseModel<string>> Handle(AddCustomizedAvatarForCustomerCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();
            var customizedAvatarId = Guid.NewGuid().ToString();

            try
            {
                await _imageKitService.UploadFileAsync(request.CustomizedAvatar, FileType.CustomizedAvatar, userId, customizedAvatarId);
            }
            catch (Exception ex)
            {
                return ResponseModel.Failure<string>($"File upload failed: {ex.Message}");
            }

            var customerEntity = new Domain.Entities.Customer();
            customerEntity.SetUserId(userId);
            customerEntity.SetBaseAvatarId(request.BaseAvatarId);
            customerEntity.SetCustomizedAvatarId(customizedAvatarId);

            await _customerRepo.AddAsync(customerEntity);
            await _customerRepo.SaveChangesAsync();

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
