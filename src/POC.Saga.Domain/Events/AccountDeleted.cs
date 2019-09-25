using System;

namespace POC.Saga.Domain.Events
{
    public class AccountDeleted : Event
    {
        public Guid AccountId { get; set; }
    }
}
