using System;

namespace POC.Saga.Domain.Events
{
    public class AccountCreated : Event
    {
        public Guid AccountId { get; set; }
        public string Email { get; set; }
    }
}
