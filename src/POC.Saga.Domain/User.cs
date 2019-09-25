using POC.Saga.Domain.Events;
using System;

namespace POC.Saga.Domain
{
    public class User : Entity<Guid>
    {
        public string Email { get; private set; }

        protected User() { }

        private User(Guid userId, string email)
            : base(userId)
        {
            Email = email;
            Events.Enqueue(new UserCreated(userId, email));
        }

        public static User Create(string email)
            => new User(Guid.NewGuid(), email);
    }
}
