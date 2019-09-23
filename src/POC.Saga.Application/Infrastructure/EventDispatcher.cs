using EnsureThat;
using MassTransit;
using POC.Saga.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TEC.CoreCommon.Domain;
using TEC.CoreCommon.Domain.Events;

namespace POC.Saga.Application.Infrastructure
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IPublishEndpoint _endpoint;
        protected readonly Queue<Event> Events = new Queue<Event>();

        public EventDispatcher(IPublishEndpoint endpoint) => _endpoint = endpoint;

        public void Push(Event domainEvent)
        {
            Ensure.Any.IsNotNull(domainEvent, nameof(domainEvent));
            if (Events.Contains(domainEvent))
                throw new InvalidOperationException($"Event {domainEvent.Id} already exists in the queue.");
            Events.Enqueue(domainEvent);
        }

        public void Push(Queue<Event> domainEvents)
        {
            while (domainEvents.TryDequeue(out var domainEvent))
                Push(domainEvent);
        }

        public void Push(Entity entity)
            => Push(entity?.Events ?? new Queue<Event>());

        public async Task DispatchAsync(CancellationToken token = default)
        {
            while (Events.TryDequeue(out var e))
                await _endpoint.Publish(e, e.GetType(), token);
        }
    }
}
