using System;

namespace POC.Saga.Domain.Commands
{
    public class DeleteInvitation
    {
        public Guid CorrelationId { get; set; }
        public Guid InvitationId { get; set; }
    }
}
