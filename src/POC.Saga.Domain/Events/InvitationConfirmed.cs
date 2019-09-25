using System;
using TEC.CoreCommon.Domain.Events;

namespace POC.Saga.Domain.Events
{
    public class InvitationConfirmed : Event
    {
        public Guid InvitationId { get; private set; }

        public InvitationConfirmed(Guid invitationId)
            => InvitationId = invitationId;
    }
}
