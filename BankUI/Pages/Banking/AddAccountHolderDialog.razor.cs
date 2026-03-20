using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Runtime.CompilerServices;

namespace BankUI.Pages.Banking
{
    public partial class AddAccountHolderDialog
    {

        [Parameter] public CreateAccountHolder CreateAccountHolderRequest { get; set; } = new();

        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        MudForm _form = default;

        public DateTime? DateOfBirth { get; set; }

        private async Task SaveAsync()
        {
            // Cast
            CreateAccountHolderRequest.DateOfBirth = (DateTime)DateOfBirth;

            var response = await _accountHolderService.AddAccountHolderAsync(CreateAccountHolderRequest);
            if (response.IsSuccessful)
            {
                _snackbar.Add(response.Messages[0], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }

        void Cancel() => MudDialog.Cancel();
    }
}