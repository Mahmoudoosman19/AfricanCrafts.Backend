using Common.Domain.Primitives;
using Order.Domain.Enum;

namespace Order.Domain.Entities
{
    public class Order :
       //  Entity<Guid>,
       AggregateRoot<Guid>,
       IAuditableEntity
    {
        private List<OrderItem> _orderItems = [];

        public string OrderNumber { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal TotalPrice { get; private set; }
        public decimal DiscountedPrice { get; private set; } = 0;
        public long StatusId { get; private set; }
        public long PaymentStatusId { get; private set; }
        public long PaymentMethodId { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public ICollection<OrderItem> Items => _orderItems;

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public DateTime CustomerReceived { get; private set; }
        public string ReturnedReason { get; set; }

        public bool IsShippingConfirms { get; private set; } = false;
        public void SetOrderNumber(string orderNumber)
        {
            OrderNumber = orderNumber;
        }
        public void SetCustomerId(Guid customerId)
        {
            CustomerId = customerId;
        }
        
        public void SetPrice(decimal totalPrice, decimal discountedPrice)
        {
            TotalPrice = totalPrice;
            DiscountedPrice = discountedPrice;
        }
        public void AddItem(OrderItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _orderItems.Add(item);
        }
        public void AddRangeItem(List<OrderItem> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            _orderItems.AddRange(items);
        }
        public void RemoveItem(OrderItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _orderItems.Remove(item);
        }
        public void SetStatus(OrderStatusEnum orderStatus)
        {
            StatusId = (long)orderStatus;
        }
        public void SetPaymentStatus(PaymentStatusEnum paymentStatus)
        {
            PaymentStatusId = (long)paymentStatus;
        }
        public void SetPaymentMethod(PaymentMethodEnum paymentMethod)
        {
            PaymentMethodId = (long)paymentMethod;
        }
        public void SetCustomerReceived()
        {
            CustomerReceived = DateTime.Now;
        }
        
        public void SetIsShippingConfirms(bool isShippingConfirms)
        {
            IsShippingConfirms = isShippingConfirms;
        }
        public void SetReturnedReason(string returnedReason)
        {
            ReturnedReason = returnedReason;
        }

        //public void AddNotification()
        //{
        //    RaiseDomainEvent(new CreateNotificationsDomainEvent(Guid.NewGuid(),OrderNumber));
        //}
    }
}
