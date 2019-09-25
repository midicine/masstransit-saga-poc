using POC.Saga.Domain;
using System;

namespace POC.Saga.Infrastructure.Events
{
    public class InvitationValidated : Event
    {
        public string Email { get; set; }
        public Guid InvitationId { get; set; }
        public string Password { get; set; }

    }
}
