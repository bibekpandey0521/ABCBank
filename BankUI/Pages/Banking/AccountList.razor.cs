using Common.Requests;
using Common.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Formats.Asn1;
using System.Reflection;

namespace BankUI.Pages.Banking
{
    public partial class AccountList
    {
        [Parameter] public int AccountHolderId { get; set; }
        public List<AccountResponse> Accounts { get; set; } = [];

        private bool _loading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadAccounts();
        }
        private async Task LoadAccounts()
        {
            var response = await _accountService.GetAccountsByAccountHolderIdAsync(AccountHolderId);
            if (response.IsSuccessful)
            {
                Accounts = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackbar.Add(message, MudBlazor.Severity.Error);
                }
            }
            _loading = false;
        }
        private void PageClosed()
        {
            _navigation.NavigateTo("/banking/account-holder-list");
        }

        private async Task AddAccountAsync()
        {
            var parameters = new DialogParameters
            {
                {nameof(AddAccountDialog.AccountHolderId),AccountHolderId}
            };
            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
                BackdropClick = true
            };

            var dialog = await _dialogService.ShowAsync<AddAccountDialog>("Open Bank Account", parameters,options);

            var result = await   dialog.Result;

            if (!result.Canceled)
            {
                // Reload accounts after successful creation
                await LoadAccounts();
            }
        }

        private async Task TransactAsync(int accountId, decimal balance)
        {
            
            var parameters = new DialogParameters
            {
                { nameof(TransactDialog.AccountId), accountId },
                { nameof(TransactDialog.Balance), balance }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
                BackdropClick = true
            };

            var dialog = await _dialogService.ShowAsync<TransactDialog>("Account Transaction", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                await LoadAccounts();
            }
        }
    }
}