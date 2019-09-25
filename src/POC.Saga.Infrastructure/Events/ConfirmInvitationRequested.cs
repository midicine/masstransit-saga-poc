using POC.Saga.Domain;
using System;

namespace POC.Saga.Infrastructure.Events
{
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
