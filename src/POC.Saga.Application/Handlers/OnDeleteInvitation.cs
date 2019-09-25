using MassTransit;
using POC.Saga.Domain.Commands;
using POC.Saga.Domain.Events;
using System.Threading.Tasks;

namespace POC.Saga.Application.Handlers
{
    public class OnDeleteInvitation : IConsumer<DeleteInvitation>
    {
        public async Task Consume(ConsumeContext<DeleteInvitation> context) 
            => await context.Publish(new InvitationConfirmed(), typeof(InvitationConfirmed), context.CancellationToken);
    }
}
