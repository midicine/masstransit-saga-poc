using System;
using TEC.CoreCommon.Domain.Events;

namespace POC.Saga.Domain.Events
{
    // TODO: not a domain event?
    public class InvitationValidated : Event
    {
        public string Email { get; private set; }
        public Guid InvitationId { get; private set; }

        public InvitationValidated(Guid invitationId, string email)
        {
            Email = email;
            InvitationId = invitationId;
        }
    }
}
