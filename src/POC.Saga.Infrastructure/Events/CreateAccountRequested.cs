//using POC.Saga.Domain.Commands;
//using System;

//namespace POC.Saga.Infrastructure.Events
//{
//    public class CreateAccountRequested
//    {
//        public CreateAccount Command { get; private set; }
//        public Guid CorrelationId { get; private set; }

//        public CreateAccountRequested(
//            Guid correlationId,
//            string email,
//            string password)
//        {
//            CorrelationId = correlationId;
//            Command = new CreateAccount(email, password);
//        }
//    }
//}
