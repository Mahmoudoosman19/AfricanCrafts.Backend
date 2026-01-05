using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;

namespace UserManagement.Application.Features.Customer.Commands.AddBaseAvatarForCustomer
{
    internal class AddBaseAvatarForCustomerHandler : ICommandHandler<AddBaseAvatarForCustomerCommand ,string>
    {
        private readonly IGenericRepository<Domain.Entities.Customer> _customerRepo;
        private readonly ITokenExtractor _tokenExtractor;
        public AddBaseAvatarForCustomerHandler(IGenericRepository<Domain.Entities.Customer> customerRepo, ITokenExtractor tokenExtractor)
        {
            _customerRepo = customerRepo;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel<string>> Handle(AddBaseAvatarForCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerId = _tokenExtractor.GetUserId();

            var customerEntity = new Domain.Entities.Customer();
            customerEntity.SetUserId(customerId);
            customerEntity.SetBaseAvatarId(request.BaseAvatarId);

            await _customerRepo.AddAsync(customerEntity);
            await _customerRepo.SaveChangesAsync();

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
