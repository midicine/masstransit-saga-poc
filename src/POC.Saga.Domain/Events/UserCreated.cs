using System;

namespace POC.Saga.Domain.Events
{
    public class UserCreated : Event
    {
        public Guid UserId { get; private set; }
        public string Email { get; private set; }
        public UserCreated(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
