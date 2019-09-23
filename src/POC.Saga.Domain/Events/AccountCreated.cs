using System;
using TEC.CoreCommon.Domain.Events;

namespace POC.Saga.Domain.Events
{
    public class AccountCreated : Event
    {
        public Guid AccountId { get; set; }
        public string Email { get; set; }

        public AccountCreated(Guid id, string email)
        {
            AccountId = id;
            Email = email;
        }
    }
}
