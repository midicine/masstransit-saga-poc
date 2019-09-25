using System;

namespace POC.Saga.Domain.Commands
{
    public class CreateAccount 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
