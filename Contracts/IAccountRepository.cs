using Entities.Models;

namespace Contracts
{
    public interface IAccountRepository
    {
        Account GetByAccountNumber(string accountNumber);
        void Update(Account account);
        IEnumerable<RechargeOperation> GetRechargeOperations(int accountId);
    }
}
