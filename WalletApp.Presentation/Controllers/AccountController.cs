using WalletApp.Presentation.Filters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared;

namespace WalletApp.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ServiceFilter(typeof(HMACAuthorizationAttribute))]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) => _accountService = accountService;

        [HttpPost("exists")]
        public IActionResult CheckAccountExists([FromHeader] HeaderParams request, string accountNumber)
        {

            if (string.IsNullOrEmpty(request.XUserId) || string.IsNullOrEmpty(request.XDigest))
            {
                return BadRequest("Invalid authentication headers.");
            }

            if (string.IsNullOrEmpty(accountNumber))
            {
                return BadRequest("Invalid account number.");
            }

            bool exists = _accountService.AccountExists(accountNumber);
            return Ok(exists);
        }

        [HttpPost("replenish")]
        public IActionResult ReplenishAccount([FromHeader] HeaderParams request,
            [FromBody] ReplenishDto replenishDto)
        {

            if (string.IsNullOrEmpty(request.XUserId) || string.IsNullOrEmpty(request.XDigest))
            {
                return BadRequest("Invalid authentication headers.");
            }


            try
            {
                bool success = _accountService.ReplenishAccount(replenishDto.AccountNumber, replenishDto.Amount);
                if (success)
                {
                    return Ok("Account replenished successfully.");
                }

                return BadRequest("Failed to replenish account.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while replenishing the account.");
            }
        }

        [HttpPost("recharge-operations")]
        public IActionResult RechargeOperationsForMonth([FromHeader] HeaderParams request,
            string accountNumber)
        {
            if (string.IsNullOrEmpty(request.XUserId) || string.IsNullOrEmpty(request.XDigest))
            {
                return BadRequest("Invalid authentication headers.");
            }

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                return BadRequest("Invalid account number.");
            }

            var result = _accountService.GetRechargeOperationsForMonth(accountNumber);
            return Ok(result);
        }

        [HttpPost("balance/{accountNumber}")]
        public IActionResult GetAccountBalance([FromHeader] HeaderParams request, string accountNumber)
        {
            if (string.IsNullOrEmpty(request.XUserId) || string.IsNullOrEmpty(request.XDigest))
            {
                return BadRequest("Invalid authentication headers.");
            }
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                return BadRequest("Invalid account number.");
            }

            decimal balance = _accountService.GetAccountBalance(accountNumber);
            return Ok(balance);
        }
    }
}
