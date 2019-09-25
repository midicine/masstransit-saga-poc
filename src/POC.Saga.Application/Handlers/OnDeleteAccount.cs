using MassTransit;
using POC.Saga.Domain.Commands;
using POC.Saga.Domain.Events;
using System.Threading.Tasks;

namespace POC.Saga.Application.Handlers
{
    public class OnDeleteAccount : IConsumer<DeleteAccount>
    {
        public async Task Consume(ConsumeContext<DeleteAccount> context)
        {
            await context.Publish(new AccountDeleted
            {
                AccountId = context.Message.AccountId,
            }, typeof(AccountDeleted), context.CancellationToken);
        }
    }
}
