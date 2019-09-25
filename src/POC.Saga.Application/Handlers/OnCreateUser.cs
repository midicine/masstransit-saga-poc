using MassTransit;
using POC.Saga.Domain;
using POC.Saga.Domain.Commands;
using System.Threading.Tasks;

namespace POC.Saga.Application.Handlers
{
    public class OnCreateUser : IConsumer<CreateUser>
    {
        public async Task Consume(ConsumeContext<CreateUser> context)
        {
            var user = User.Create(context.Message.Email);
            foreach (var ev in user.Events) 
                await context.Publish(ev, ev.GetType(), context.CancellationToken);
        }
    }
}
