using Common.Domain.DomainEvents;
using Common.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.DomainEvents
{
    public sealed record UserCreatedEvent(Guid Id, User user, string RoleName) : DomainEvent(Id)
    {  
    }
}
