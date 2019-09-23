namespace POC.Saga.Domain.Commands
{
    public class CreateUser
    {
        public string Email { get; set; }
        public CreateUser(string email) => Email = email;
    }
}
