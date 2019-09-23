﻿using POC.Saga.Domain.Events;
using System;
using TEC.CoreCommon.Domain;

namespace POC.Saga.Domain
{
    public class Account : Entity<Guid>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        protected Account() { }

        private Account(Guid id, string email, string password)
            : base(id)
        {
            Email = email;
            Password = password;
            Events.Enqueue(new AccountCreated(Id, email));
        }

        public static Account Create(string email, string password)
            => new Account(Guid.NewGuid(), email, password);
    }
}
