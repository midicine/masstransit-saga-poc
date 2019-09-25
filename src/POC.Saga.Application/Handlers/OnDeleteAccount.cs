using MassTransit;
using POC.Saga.Domain.Commands;
using POC.Saga.Domain.Events;
using POC.Saga.Infrastructure;
using System.Threading.Tasks;

namespace POC.Saga.Application.Handlers
{
    public class OnDeleteAccount : IConsumer<DeleteAccount>
    {
        private readonly IEventDispatcher _dispatcher;

        public OnDeleteAccount(IEventDispatcher dispatcher)
            => _dispatcher = dispatcher;

        public async Task Consume(ConsumeContext<DeleteAccount> context)
        {
            _dispatcher.Push(new AccountDeleted
            {
                AccountId = context.Message.AccountId,
                CorrelationId = context.Message.CorrelationId
            });
            await _dispatcher.DispatchAsync(context.CancellationToken);
        }
    }
}
