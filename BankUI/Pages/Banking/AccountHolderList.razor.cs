using Common.Requests;
using Common.Responses;
using MudBlazor;
using System.Runtime.CompilerServices;
namespace BankUI.Pages.Banking
{
    public partial class AccountHolderList
    {
        public List<AccountHolderResponse> AccountHolders { get; set; } = [];

        private bool _loading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadAccountHoldersAsync();

        }
        private async Task LoadAccountHoldersAsync()
        {
            var response = await _accountHolderService.GetAllAccountHoldersAsync();
            if (response.IsSuccessful)
            {
                AccountHolders = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }
            _loading = false;
        }

        private async Task AddAccountHolderAsync()
        {
            var parameters = new DialogParameters();
            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                BackdropClick = true
            };
            var dialog = await _dialogService.ShowAsync<AddAccountHolderDialog>("Add Account Holder", parameters, options);

            var result = await dialog.Result;
            if (!result.Canceled)
            {
                // Reload the table data 
                await LoadAccountHoldersAsync();

            }
        }

        private async Task UpdateAccountHolderAsync(int accountHolderId)
        {
            var parameters = new DialogParameters();
            var accountHolder = AccountHolders.FirstOrDefault(accountHolder => accountHolder.Id == accountHolderId);

            parameters.Add(nameof(UpdateAccountHolderDialog.UpdateAccountHolderRequest),new UpdateAccountHolder
            {
                Id = accountHolderId,
                FirstName = accountHolder.FirstName,
                LastName = accountHolder.LastName,
                ContactNumber = accountHolder.ContactNumber,
                Email = accountHolder.Email
            });

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                BackdropClick = false
            };
            var dialog =  await _dialogService.ShowAsync<UpdateAccountHolderDialog>("Update Account Holder", parameters, options);

            var result = await dialog.Result;
            if (!result.Canceled)
            {
                await LoadAccountHoldersAsync();
            }
        }
    }
}
