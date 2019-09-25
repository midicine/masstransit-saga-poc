using System;

namespace POC.Saga.Domain.Commands
{
    public class DeleteAccount
    {
        public Guid AccountId { get; set; }
    }
}
