using Application.Features.AccountHolders.Queries;
using Application.Features.Accounts.Commands;
using Application.Features.Accounts.Queries;
using Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseApiController
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddAccountAsync([FromBody] CreateAccountRequest createAccount)
        {
            var response = await Sender.Send(new CreateAccountCommand() { CreateAccount = createAccount });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetAccountByIdAsync(int id)
        {
            var response = await Sender.Send(new GetAccountByIdQuery() { Id = id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
        
        [HttpGet("by-account-number/{accountNumber}")]
        public async Task<IActionResult> GetAccountByAccountNumberAsync(string accountNumber)
        {
            var response = await Sender.Send(new GetAccountByAccountNumberQuery() { AccountNumber = accountNumber });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAccountAsync()
        {
            var response = await Sender.Send(new GetAccountsQuery());
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
