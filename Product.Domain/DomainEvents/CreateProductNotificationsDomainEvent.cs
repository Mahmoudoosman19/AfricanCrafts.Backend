using Bus.Contracts.Enum;
using Bus.Contracts.Models.Notifications;
using Common.Domain.DomainEvents;
using Product.Domain.Enums;

namespace Product.Domain.DomainEvents
{
    public sealed record CreateProductNotificationsDomainEvent(
        Guid Id,NotificationsModel notifications) : DomainEvent(Id);

}
