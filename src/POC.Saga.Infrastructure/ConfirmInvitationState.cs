using Automatonymous;
using System;

namespace POC.Saga.Infrastructure
{
    public class ConfirmInvitationState : SagaStateMachineInstance
    {
        public string CurrentState { get; set; }
        public Guid CorrelationId { get; set; }
        public string Email { get; set; }
        public Guid InvitationId { get; set; }
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime ConfirmedDate { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
