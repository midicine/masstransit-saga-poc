using System;
using TEC.CoreCommon.Domain.Events;

namespace POC.Saga.Domain.Events
{
    // TODO: not a domain event?
    public class InvitationConfirmed : Event
    {
        public Guid InvitationId { get; private set; }
        public DateTime Date { get; private set; }

        public InvitationConfirmed(Guid invitationId)
        {
            InvitationId = invitationId;
            Date = DateTime.UtcNow;
        }
    }
}
