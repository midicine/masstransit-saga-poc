using System;
using MassTransit;
using POC.Saga.Domain;
using POC.Saga.Domain.Commands;
using POC.Saga.Infrastructure;
using System.Threading.Tasks;

namespace POC.Saga.Application.Handlers
{
    public class OnCreateUser : IConsumer<CreateUser>
    {
        private readonly IEventDispatcher _dispatcher;

        public OnCreateUser(IEventDispatcher dispatcher)
            => _dispatcher = dispatcher;

        public async Task Consume(ConsumeContext<CreateUser> context)
        {
            var user = User.Create(context.Message.Email);
            _dispatcher.Push(user);
            //if (user != null) throw new Exception("test exception");
            await _dispatcher.DispatchAsync(context.CancellationToken);
        }
    }
}
