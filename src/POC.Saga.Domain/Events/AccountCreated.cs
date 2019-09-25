using System;
using TEC.CoreCommon.Domain.Events;

namespace POC.Saga.Domain.Events
{
    public class AccountCreated : Event
    {
        public Guid AccountId { get; private set; }
        public string Email { get; private set; }

        public AccountCreated(Guid id, string email)
        {
            AccountId = id;
            Email = email;
        }
    }
}
