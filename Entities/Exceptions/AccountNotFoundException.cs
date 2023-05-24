namespace Entities.Exceptions
{
    public class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException(string account) : 
            base($"Account with number {account} not found.")
        {
            
        }
    }
}
