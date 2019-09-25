using Automatonymous;
using POC.Saga.Domain.Commands;
using POC.Saga.Domain.Events;
using POC.Saga.Infrastructure.Events;
using System;

namespace POC.Saga.Infrastructure
{
    public class ConfirmInvitationStateMachine :
        MassTransitStateMachine<ConfirmInvitationSaga>
    {
        //public Event<CreateAccountRequested> CreateAccountEvent { get; private set; }
        internal Event<AccountCreated> AccountCreated { get; private set; }
        public Event<UserCreated> UserCreated { get; private set; }
        public Event<InvitationConfirmed> InvitationConfirmed { get; private set; }
        public Event<InvitationValidated> InvitationValidated { get; private set; }

        public State InvitationValidatedState { get; private set; }
        public State AccountCreatedState { get; private set; }
        public State UserCreatedState { get; private set; }

        public ConfirmInvitationStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => InvitationValidated,
                conf => conf
                    .CorrelateBy(x => x.Email, x => x.Message.Email)
                    .SelectId(x => x.Message.CorrelationId));

            Event(() => AccountCreated,
                conf => conf.CorrelateById(x => x.Message.CorrelationId));

            Event(() => UserCreated,
                conf => conf.CorrelateById(x => x.Message.CorrelationId));

            Event(() => InvitationConfirmed,
                conf => conf.CorrelateById(x => x.Message.CorrelationId));

            Initially(
                When(InvitationValidated)
                    .Then(context =>
                    {
                        context.Instance.Email = context.Data.Email;
                        context.Instance.InvitationId = context.Data.InvitationId;
                    })
                    .ThenAsync(x =>
                    {
                        return x.Send(new CreateAccount
                        {
                            CorrelationId = x.Instance.CorrelationId,
                            Email = x.Data.Email,
                            Password = x.Data.Password
                        });
                    }),

                When(AccountCreated)
                    .Then(context => { context.Instance.AccountId = context.Data.AccountId; })
                    .Send(x => new CreateUser
                    {
                        CorrelationId = x.Instance.CorrelationId,
                        Email = x.Instance.Email
                    }),

                When(UserCreated)
                    .Then(context => { context.Instance.UserId = context.Data.UserId; })
                    .Send(x => new DeleteInvitation
                    {
                        CorrelationId = x.Instance.CorrelationId,
                        InvitationId = x.Instance.InvitationId
                    }),

                When(InvitationConfirmed)
                    .Then(context =>
                    {
                        context.Instance.ConfirmedDate = DateTime.UtcNow;
                    })
                    .Finalize()
                );

            SetCompletedWhenFinalized();
        }
    }
}
