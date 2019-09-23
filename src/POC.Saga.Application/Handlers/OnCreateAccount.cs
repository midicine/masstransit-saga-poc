using MassTransit;
using POC.Saga.Domain;
using POC.Saga.Domain.Commands;
using POC.Saga.Infrastructure;
using System.Threading.Tasks;

namespace POC.Saga.Application.Handlers
{
    public class OnCreateAccount : IConsumer<CreateAccount>
    {
        private readonly IEventDispatcher _dispatcher;

        public OnCreateAccount(IEventDispatcher dispatcher)
            => _dispatcher = dispatcher;

        public async Task Consume(ConsumeContext<CreateAccount> context)
        {
            var account = Account.Create(context.Message.Email, context.Message.Password);
            _dispatcher.Push(account);
            await _dispatcher.DispatchAsync(context.CancellationToken);
        }
    }
}
