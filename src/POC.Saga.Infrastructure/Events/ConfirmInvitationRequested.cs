using POC.Saga.Domain;
using System;

namespace POC.Saga.Infrastructure.Events
{
    public class ConfirmInvitationRequested : Event
    { 
        public Guid InvitationId { get; set; }
        public string Password { get; set; }
    }
}
