using System;

namespace POC.Saga.Domain.Commands
{
    public class CreateUser
    {
        public Guid CorrelationId { get; set; }
        public string Email { get; set; }
    }
}
