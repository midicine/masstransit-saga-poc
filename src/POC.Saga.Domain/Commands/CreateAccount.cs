namespace POC.Saga.Domain.Commands
{
    public class CreateAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public CreateAccount(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
