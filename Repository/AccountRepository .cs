using Contracts;
using Entities.Models;
using WalletApp.Data;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly WalletDbContext _dbContext;

        public AccountRepository(WalletDbContext dbContext) => _dbContext = dbContext;
        public Account GetByAccountNumber(string accountNumber)
        {
            return _dbContext.Accounts.FirstOrDefault(a=>a.AccountNumber == accountNumber);
        }

        public IEnumerable<RechargeOperation> GetRechargeOperations(int accountId)
        {
            return _dbContext.RechargeOperations.Where(a => a.AccountId == accountId);
        }

        public void Update(Account account)
        {
            _dbContext.Accounts.Update(account);
            _dbContext.SaveChanges();
        }
    }
}
