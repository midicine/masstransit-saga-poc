using System;
using TEC.CoreCommon.Domain.Events;

namespace POC.Saga.Domain.Events
{
    public class AccountDeleted : Event
    {
        public Guid AccountId { get; }
        public AccountDeleted(Guid accountId) => AccountId = accountId;
    }
}
