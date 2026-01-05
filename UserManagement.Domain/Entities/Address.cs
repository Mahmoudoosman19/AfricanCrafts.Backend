using Common.Domain.Primitives;

namespace UserManagement.Domain.Entities
{
    public class Address : Entity<Guid>, IAuditableEntity,ISoftDeleteEntity
    {
        public Guid UserId { get; set; }
        public string Name {  get; set; }   
        public string PhoneNumber {  get; set; }
        public string City { get; set; }
        public string? Floor { get; set; } = null;
        public string AddressName  { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get ; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DateTime? RestoredAt { get; set; }
        public void setData(string name,string addressName,string city, string phoneNumber,string floor,Guid userId)
        {
            Name = name;
            PhoneNumber = phoneNumber;  
            Floor = floor;
            City = city;    
            AddressName = addressName; 
            UserId = userId;    
        }
        public void Restored()
        {
            IsDeleted = false;
        }
    }
}
