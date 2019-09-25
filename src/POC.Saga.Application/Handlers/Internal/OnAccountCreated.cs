//using System.Threading.Tasks;
//using MassTransit;
//using POC.Saga.Domain.Events;
//using POC.Saga.Infrastructure.Events;

//namespace POC.Saga.Application.Handlers.Internal
//{
//    public class OnAccountCreated : IConsumer<AccountCreated>
//    {
//        public async Task Consume(ConsumeContext<AccountCreated> context)
//        {
//            await context.Publish(
//                new AccountCreatedEvent(context.InitiatorId.Value, context.Message.AccountId, context.Message.Email),
//                context.CancellationToken);
//        }
//    }
//}
