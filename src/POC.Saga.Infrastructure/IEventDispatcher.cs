using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TEC.CoreCommon.Domain;
using TEC.CoreCommon.Domain.Events;

namespace POC.Saga.Infrastructure
{
    public interface IEventDispatcher
    {
        void Push(Event domainEvent);
        void Push(Queue<Event> domainEvents);
        void Push(Entity entity);
        Task DispatchAsync(CancellationToken token = default);
    }
}
