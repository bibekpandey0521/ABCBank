using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace BankUI.Services
{
    public interface IAccountService
    {
        Task<ResponseWrapper<int>> AddAccountAsync(CreateAccountRequest createAccount);
        Task<ResponseWrapper<int>> TransactionAccountAsync(TransactionRequest transaction);

        Task<ResponseWrapper<AccountResponse>> GetAccountByIdAsync(int id);
        Task<ResponseWrapper<AccountResponse>> GetAccountByAccountNumberAsync(string accountNumber);
        Task<ResponseWrapper<List<TransactionResponse>>> GetAccountTransactionsAsync(int accountId);

        Task<ResponseWrapper<List<AccountResponse>>> GetAllAccountsAsync();
    }
}
