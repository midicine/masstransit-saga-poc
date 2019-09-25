using MassTransit;
using POC.Saga.Infrastructure;
using POC.Saga.Infrastructure.Events;
using System.Threading.Tasks;

namespace POC.Saga.Application.Handlers
{
    public class OnConfirmInvitationRequested : IConsumer<ConfirmInvitationRequested>
    {
        private readonly IEventDispatcher _dispatcher;

        public OnConfirmInvitationRequested(IEventDispatcher dispatcher)
            => _dispatcher = dispatcher;

        public async Task Consume(ConsumeContext<ConfirmInvitationRequested> context)
        {
            // GET invitation from repository, get email

            _dispatcher.Push(new InvitationValidated
            {
                CorrelationId = context.Message.CorrelationId,
                InvitationId = context.Message.InvitationId,
                Email = "hapica@gmail.com",
                Password = context.Message.Password
            });
            await _dispatcher.DispatchAsync(context.CancellationToken);
        }
    }
}
