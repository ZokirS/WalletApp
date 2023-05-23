using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public bool AccountExists(string accountNumber)
        {
            var account = _repository.GetByAccountNumber(accountNumber);
            return account != null;
        }

        public decimal GetAccountBalance(string accountNumber)
        {
            var account = _repository.GetByAccountNumber(accountNumber);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            return account.Balance;
        }

        public IEnumerable<RechargeOperationDto> GetRechargeOperationsForMonth(string accountNumber)
        {
            var account = _repository.GetByAccountNumber(accountNumber);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }


            var operationsDto =  _repository.GetRechargeOperations(account.Id)
                .Where(ro => ro.Date.Month == DateTime.Now.Month);
            var operationsToReturn = _mapper.Map<IEnumerable<RechargeOperationDto>>(operationsDto);
            return operationsToReturn;
        }

        public bool ReplenishAccount(string accountNumber, decimal amount)
        {
            var account = _repository.GetByAccountNumber(accountNumber);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            decimal newBalance = account.Balance + amount;
            if (account.IsIdentified == false && newBalance > 10000)
            {
                throw new Exception("Exceeded maximum balance for unidentified accounts.");
            }
            else if (account.IsIdentified == true && newBalance > 100000)
            {
                throw new Exception("Exceeded maximum balance for identified accounts.");
            }

            account.Balance = newBalance;
            _repository.Update(account);
            return true;
        }
    }
}
