using System;

namespace POC.Saga.Domain.Commands
{
    public class ConfirmInvitation
    {
        public Guid RequestId { get; set; }
        public Guid InvitationId { get; set; }
        public string Password { get; set; }

        public ConfirmInvitation(Guid requestId, Guid invitationId, string password)
        {
            RequestId = requestId;
            InvitationId = invitationId;
            Password = password;
        }
    }
}
