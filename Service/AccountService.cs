using AutoMapper;
using Contracts;
using Entities.Exceptions;
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
                throw new AccountNotFoundException(accountNumber);
            }

            return account.Balance;
        }

        public IEnumerable<RechargeOperationDto> GetRechargeOperationsForMonth(string accountNumber)
        {
            var account = _repository.GetByAccountNumber(accountNumber);
            if (account == null)
            {
                throw new AccountNotFoundException(accountNumber);
            }


            var operationsEntity =  _repository.GetRechargeOperations(account.Id)
                .Where(ro => ro.Date.Month == DateTime.Now.Month);
            var operationsToReturn = _mapper.Map<IEnumerable<RechargeOperationDto>>(operationsEntity);
            return operationsToReturn;
        }

        public bool ReplenishAccount(string accountNumber, decimal amount)
        {
            var account = _repository.GetByAccountNumber(accountNumber);
            if (account == null)
            {
                throw new AccountNotFoundException(accountNumber);
            }

            decimal newBalance = account.Balance + amount;
            if (account.IsIdentified == false && newBalance > 10000)
            {
                throw new ExceededMaxException();
            }
            else if (account.IsIdentified == true && newBalance > 100000)
            {
                throw new ExceedMinException();
            }

            account.Balance = newBalance;
            _repository.Update(account);
            return true;
        }
    }
}
