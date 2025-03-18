using AmmFramework.Core.Domain.Events;

namespace AmmFramework.Core.Contracts.ApplicationServices.Events;

public interface IEventDispatcher
{
    Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent;
}