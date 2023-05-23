using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IAccountService
    {
        bool AccountExists(string accountNumber);
        bool ReplenishAccount(string accountNumber, decimal amount);
        IEnumerable<RechargeOperationDto> GetRechargeOperationsForMonth(string accountNumber);
        decimal GetAccountBalance(string accountNumber);
    }
}
