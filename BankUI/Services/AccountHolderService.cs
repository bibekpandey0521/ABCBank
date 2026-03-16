using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace BankUI.Services
{
    public class AccountHolderService : IAccountHolderService
    {
       
        public Task<ResponseWrapper<int>> AddAccountHolderAsync(CreateAccountHolder createAccountHolder)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseWrapper<int>> DeleteAccountHolderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseWrapper<AccountHolderResponse>> GetAccountHolderByIdResponse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseWrapper<List<AccountHolderResponse>>> GetAllAccountHoldersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseWrapper<int>> UpdateAccountHolderAsync(UpdateAccountHolder updateAccountHolder)
        {
            throw new NotImplementedException();
        }
    }

}
