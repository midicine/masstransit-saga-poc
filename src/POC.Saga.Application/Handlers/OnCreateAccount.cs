﻿using MassTransit;
using POC.Saga.Domain;
using POC.Saga.Domain.Commands;
using POC.Saga.Infrastructure;
using System.Threading.Tasks;

namespace POC.Saga.Application.Handlers
{
    public class OnCreateAccount : IConsumer<CreateAccount>
    {
        public async Task Consume(ConsumeContext<CreateAccount> context)
        {
            var account = Account.Create(context.Message.Email, context.Message.Password);
            foreach (var ev in account.Events)
                await context.Publish(ev, ev.GetType(), context.CancellationToken);
        }
    }
}
