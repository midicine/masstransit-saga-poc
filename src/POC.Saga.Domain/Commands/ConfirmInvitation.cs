using System;

namespace POC.Saga.Domain.Commands
{
    public class ConfirmInvitation
    {
        public Guid InvitationId { get; set; }
        public string Password { get; set; }
    }
}
