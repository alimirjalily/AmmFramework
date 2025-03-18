using AmmFramework.Core.Domain.Events;

namespace AmmFramework.Core.Contracts.ApplicationServices.Events;

public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent Event);
}