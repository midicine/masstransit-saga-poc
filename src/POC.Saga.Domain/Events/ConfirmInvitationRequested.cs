using System;
using TEC.CoreCommon.Domain.Events;

namespace POC.Saga.Domain.Events
{
    // TODO: not a domain event?
    public class ConfirmInvitationRequested : Event
    {
        public Guid RequestId { get; private set; }
        public Guid InvitationId { get; private set; }
        public string Password { get; private set; }

        public ConfirmInvitationRequested(Guid requestId, Guid invitationId, string password)
        {
            RequestId = requestId;
            InvitationId = invitationId;
            Password = password;
        }
    }
}
