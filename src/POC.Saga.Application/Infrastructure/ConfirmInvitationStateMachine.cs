using Automatonymous;
using POC.Saga.Domain.Commands;
using POC.Saga.Domain.Events;
using System;

namespace POC.Saga.Application.Infrastructure
{
    public class ConfirmInvitationStateMachine :
        MassTransitStateMachine<ConfirmInvitation>
    {
        public Event<AccountCreated> AccountCreated { get; private set; }
        public Event<UserCreated> UserCreated { get; private set; }
        public Event<InvitationConfirmed> InvitationConfirmed { get; private set; }
        public Event<ConfirmInvitationRequested> ConfirmInvitationRequested { get; private set; }
        public Event<InvitationValidated> InvitationValidated { get; private set; }

        public State InvitationValidatedState { get; private set; }
        public State AccountCreatedState { get; private set; }
        public State UserCreatedState { get; private set; }

        public ConfirmInvitationStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => ConfirmInvitationRequested,
                conf => conf
                    .CorrelateBy<Guid>(x => x.InvitationId, x => x.Message.InvitationId)
                    .SelectId(x => x.Message.RequestId));

            Event(() => InvitationValidated,
                conf => conf
                    .CorrelateById(x => x.Message.InvitationId)
                    .CorrelateBy(x => x.Email, x => x.Message.Email));

            Event(() => AccountCreated,
                conf => conf.CorrelateBy(x => x.Email, x => x.Message.Email));

            Event(() => UserCreated,
                conf => conf.CorrelateBy(x => x.Email, x => x.Message.Email));

            Event(() => InvitationConfirmed,
                conf => conf.CorrelateBy<Guid>(x => x.InvitationId, x => x.Message.InvitationId));

            Initially(
                When(ConfirmInvitationRequested)
                    .Then(x =>
                    {
                        x.Instance.Password = x.Data.Password;
                        x.Instance.InvitationId = x.Data.InvitationId;
                    }),

                When(InvitationValidated)
                    .Then(context =>
                    {
                        context.Instance.Email = context.Data.Email;
                    })
                    .TransitionTo(InvitationValidatedState)
                    .ThenAsync(async context =>
                    {
                        await context.Send(new CreateAccount(context.Data.Email, context.Instance.Password));
                    })
                );

            During(InvitationValidatedState,
                When(AccountCreated)
                    .Then(context => context.Instance.AccountId = context.Data.AccountId)
                    .TransitionTo(AccountCreatedState)
                    .ThenAsync(async context =>
                    {
                        await context.Send(new CreateUser(context.Data.Email));
                    }));

            During(AccountCreatedState,
                When(UserCreated)
                    .Then(context => context.Instance.UserId = context.Data.UserId)
                    .TransitionTo(UserCreatedState)
                    .ThenAsync(async context =>
                    {
                        await context.Send(new DeleteInvitation(context.Instance.CorrelationId));
                    }));

            During(UserCreatedState,
                When(InvitationConfirmed)
                    .Then(context =>
                        Console.WriteLine(
                            $"--------------- InvitationConfirmed '{context.Instance.CorrelationId}' -----------------"))
                    .Finalize());

            SetCompletedWhenFinalized();
        }
    }
}
