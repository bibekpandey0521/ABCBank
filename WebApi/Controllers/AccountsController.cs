using Application.Features.AccountHolders.Queries;
using Application.Features.Accounts.Commands;
using Application.Features.Accounts.Queries;
using Common.Requests;
using Mapster;
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

        [HttpPost("transact")]
        public async Task<IActionResult> TransactionAsync([FromBody] TransactionRequest transaction)
        {
            var response = await Sender.Send(new CreateTransactionCommand()
            {
                Transaction = transaction
            });
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
        [HttpGet("transactions/{accountId}")]
        public async Task<IActionResult> GetAccountTransactionsAsync(int accountId)
        {
            var response = await Sender.Send(new GetAccountTransactionQuery() { AccountId = accountId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("by-account-holder/{accountHolderId}")]
        public async Task<IActionResult> GetAccountsByAccountHolderIdAsync(int accountHolderId)
        {
            var response = await Sender.Send(new GetAccountsByAccountHolderId { AccountHolderId = accountHolderId } );
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
