using Common.Responses;
using MudBlazor;
namespace BankUI.Pages.Banking
{
    public partial class AccountHolderList
    {
        public List<AccountHolderResponse> AccountHolders { get; set; } = [];

        private bool _loading = true;

        protected override async Task OnInitializedAsync()
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


        private async Task AddAccuntHolderAsync()
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
                // Reload Account Holder List
                await Console.Out.WriteLineAsync("All went well");
            }
        }
    }
}
