using System;
using TEC.CoreCommon.Domain.Events;

namespace POC.Saga.Infrastructure.Events
{
    public class InvitationValidated : Event
    {
        public Guid RequestId { get; private set; }
        public string Email { get; private set; }
        public Guid InvitationId { get; private set; }
        public string Password { get; private set; }

        public InvitationValidated(
            Guid requestId,
            Guid invitationId,
            string email,
            string password)
        {
            Email = email;
            RequestId = requestId;
            InvitationId = invitationId;
            Password = password;
        }
    }
}
