using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.Customer
{
    public class ListCustomerAddressSpecification : Specification<Domain.Entities.Address>
    {
        public ListCustomerAddressSpecification(Guid UserId) 
        {
            AddCriteria(c=>c.UserId == UserId&&!c.IsDeleted);
        }
    }
}
