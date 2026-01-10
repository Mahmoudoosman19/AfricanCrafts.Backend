using Common.Domain.Repositories;
using Common.Domain.Shared;
using UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Abstract;
using UserManagement.Application.Specifications.User;
using UserManagement.Domain.Abstraction;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Type
{
    internal class ConfirmUserPhoneType : BaseConfirmOTP
    {
        public ConfirmUserPhoneType(CustomUserManager userManager,
            IUserRepository<Domain.Entities.OTP> OTPRepo,
            IUserRepository<Domain.Entities.User> userRepo) : base(userManager, OTPRepo, userRepo)
        {
        }

        public override OTPType Type { get; set; } = OTPType.ConfirmPhoneNumber;

        public override async Task<ResponseModel> ConfirmOTP(ConfirmOTPCommand command)
        {
            var user = _userRepo.GetEntityWithSpec(new GetUserByEmailWithOtpSpecification(command.Email!));
            
            var otp = user!.Otp;

            user!.ConfirmPhoneNumber();
            
            await _userManager.UpdateAsync(user);

            otp.MarkAsUsed();

            await _userRepo.SaveChangesAsync();

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
