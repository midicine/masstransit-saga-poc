using System;

namespace POC.Saga.Domain
{
    public abstract class Event
    {
        public Guid CorrelationId { get; set; }
    }
}