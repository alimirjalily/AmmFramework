using AmmFramework.Core.Domain.Events;

namespace AmmFramework.Core.Domain.Entities
{
    public interface IAggregateRoot
    {
        void ClearEvents();
        IEnumerable<IDomainEvent> GetEvents();
    }
}