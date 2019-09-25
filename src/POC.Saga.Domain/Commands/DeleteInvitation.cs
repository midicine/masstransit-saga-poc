using System;

namespace POC.Saga.Domain.Commands
{
    public class DeleteInvitation
    {
        public Guid InvitationId { get; set; }
    }
}
