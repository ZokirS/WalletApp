using WalletApp.Presentation.Filters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared;
using Microsoft.AspNetCore.Http;

namespace WalletApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) => _accountService = accountService;

        [HttpPost("exists")]
        public IActionResult CheckAccountExists([FromBody] CheckAccountDto request, [FromHeader(Name = "Custom-Params")] HeaderParams headerParams)
        {
            if (string.IsNullOrEmpty(request.AccountNumber))
            {
                return BadRequest("Invalid account number.");
            }

            bool exists = _accountService.AccountExists(request.AccountNumber);
            return Ok(exists);
        }

        [HttpPost("replenish")]
        public IActionResult ReplenishAccount([FromHeader(Name = "Custom-Params")] HeaderParams headerParams, 
            [FromBody] ReplenishDto request)
        {
            try
            {
                bool success = _accountService.ReplenishAccount(request.AccountNumber, request.Amount);
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
        public IActionResult RechargeOperationsForMonth([FromHeader(Name = "Custom-Params")] HeaderParams headerParams,
            [FromBody]RechargeOperationsForMonthDto request)
        {
            if (string.IsNullOrWhiteSpace(request.accountNumber))
            {
                return BadRequest("Invalid account number.");
            }

            var result = _accountService.GetRechargeOperationsForMonth(request.accountNumber);
            return Ok(result);
        }

        [HttpPost("balance")]
        public IActionResult GetAccountBalance([FromHeader(Name = "Custom-Params")] HeaderParams headerParams, 
            [FromBody] GetBalanceDto request)
        {
            if (string.IsNullOrWhiteSpace(request.accountNumber))
            {
                return BadRequest("Invalid account number.");
            }

            decimal balance = _accountService.GetAccountBalance(request.accountNumber);
            return Ok(balance);
        }
    }
}
