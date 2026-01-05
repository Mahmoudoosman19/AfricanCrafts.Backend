using Common.Domain.Specification;
using UserManagement.Application.Features.Customer.Commands.CustomerDeletedAddressById;

namespace UserManagement.Application.Specifications.Customer
{
    public class DeleteAddressSpecification : Specification<Domain.Entities.Address>
    {
        public DeleteAddressSpecification( DeleteAddressByIdCommand delete,Guid userId) 
        {
            AddCriteria(c=>c.Id==delete.Id&&c.UserId==userId);
        }
    }
}
