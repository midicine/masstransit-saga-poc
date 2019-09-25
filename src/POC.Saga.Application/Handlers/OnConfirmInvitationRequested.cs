using System;
using MassTransit;
using POC.Saga.Infrastructure;
using POC.Saga.Infrastructure.Events;
using System.Threading.Tasks;

namespace POC.Saga.Application.Handlers
{
    public class OnConfirmInvitationRequested : IConsumer<ConfirmInvitationRequested>
    {
        public async Task Consume(ConsumeContext<ConfirmInvitationRequested> context)
        {
            // GET invitation from repository, get email
            await context.Publish(
                new InvitationValidated
                {
                    InvitationId = context.Message.InvitationId,
                    Email = "hapica@gmail.com",
                    Password = context.Message.Password
                },
                typeof(InvitationValidated),
                context.CancellationToken);
        }
    }
}
