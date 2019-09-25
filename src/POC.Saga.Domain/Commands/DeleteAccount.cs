using System;

namespace POC.Saga.Domain.Commands
{
    public class DeleteAccount
    {
        public Guid CorrelationId { get; set; }
        public Guid AccountId { get; set; }
    }
}
