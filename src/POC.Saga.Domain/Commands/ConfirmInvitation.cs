using System;

namespace POC.Saga.Domain.Commands
{
    public class ConfirmInvitation
    {
        public Guid CorrelationId { get; set; }
        public Guid InvitationId { get; set; }
        public string Password { get; set; }
    }
}
