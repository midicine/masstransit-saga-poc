using MassTransit;
using POC.Saga.Domain.Commands;
using POC.Saga.Domain.Events;
using POC.Saga.Infrastructure;
using System.Threading.Tasks;

namespace POC.Saga.Application.Handlers
{
    public class OnDeleteInvitation : IConsumer<DeleteInvitation>
    {
        private readonly IEventDispatcher _dispatcher;

        public OnDeleteInvitation(IEventDispatcher dispatcher)
            => _dispatcher = dispatcher;

        public async Task Consume(ConsumeContext<DeleteInvitation> context)
        {
            _dispatcher.Push(new InvitationConfirmed(context.Message.InvitationId));
            await _dispatcher.DispatchAsync(context.CancellationToken);
        }
    }
}
