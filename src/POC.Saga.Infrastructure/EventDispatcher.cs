﻿using EnsureThat;
using MassTransit;
using POC.Saga.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Saga.Infrastructure
{
    public interface IEventDispatcher
    {
        void Push(Event domainEvent);
        void Push(Queue<Event> domainEvents);
        void Push(Entity entity);
        Task DispatchAsync(CancellationToken token = default);
    }

    public class EventDispatcher : IEventDispatcher
    {
        private readonly IPublishEndpoint _endpoint;
        protected readonly Queue<Event> Events = new Queue<Event>();

        public EventDispatcher(IPublishEndpoint endpoint) => _endpoint = endpoint;

        public void Push(Event domainEvent)
        {
            Ensure.Any.IsNotNull(domainEvent, nameof(domainEvent));
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
