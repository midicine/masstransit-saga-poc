using Automatonymous;
using POC.Saga.Domain.Commands;
using POC.Saga.Domain.Events;
using POC.Saga.Infrastructure.Events;
using System;

namespace POC.Saga.Infrastructure
{
    public class ConfirmInvitationStateMachine : MassTransitStateMachine<ConfirmInvitationState>
    {
        internal Event<AccountCreated> AccountCreated { get; private set; }
        public Event<UserCreated> UserCreated { get; private set; }
        public Event<InvitationConfirmed> InvitationConfirmed { get; private set; }
        public Event<InvitationValidated> InvitationValidated { get; private set; }

        public ConfirmInvitationStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => InvitationValidated,
                conf => conf
                    .CorrelateBy<Guid>(x => x.InvitationId, x => x.Message.InvitationId)
                    .SelectId(x => x.ConversationId.GetValueOrDefault()));

            Event(() => AccountCreated,
                conf => conf.CorrelateById(x => x.ConversationId.GetValueOrDefault()));

            Event(() => UserCreated,
                conf => conf.CorrelateById(x => x.ConversationId.GetValueOrDefault()));

            Event(() => InvitationConfirmed,
                conf => conf.CorrelateById(x => x.ConversationId.GetValueOrDefault()));

            Initially(
                When(InvitationValidated)
                    .Then(x =>
                    {
                        x.Instance.Email = x.Data.Email;
                        x.Instance.InvitationId = x.Data.InvitationId;
                    })
                    .ThenAsync(x => x.Send(new CreateAccount
                    {
                        Email = x.Data.Email,
                        Password = x.Data.Password
                    })),

                When(AccountCreated)
                    .Then(x =>
                    {
                        x.Instance.AccountId = x.Data.AccountId;
                    })
                    .Send(x => new CreateUser
                    {
                        Email = x.Data.Email
                    }),

                When(UserCreated)
                    .Then(x =>
                    {
                        x.Instance.UserId = x.Data.UserId;
                    })
                    .Send(x => new DeleteInvitation
                    {
                        InvitationId = x.Instance.InvitationId
                    }),

                When(InvitationConfirmed)
                    .Then(x =>
                    {
                        x.Instance.ConfirmedDate = DateTime.UtcNow;
                    })
                    .Finalize()
                );

            SetCompletedWhenFinalized();
        }
    }
}
