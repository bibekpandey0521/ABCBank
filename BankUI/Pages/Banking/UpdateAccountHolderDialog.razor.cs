using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking
{
    public partial class UpdateAccountHolderDialog
    {
        [Parameter] public UpdateAccountHolder UpdateAccountHolderRequest { get; set; } = new();


        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }


        MudForm _form = default;

        public DateTime? DateOfBirth { get; set; }

        public async Task SaveAsync()
        {
            var response = await _accountHolderService.UpdateAccountHolderAsync(UpdateAccountHolderRequest);
            if (response.IsSuccessful)
            {
                _snackbar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach(var message in response.Messages)
                {
                    _snackbar.Add(message,Severity.Error);
                }
            }
        }

        void Cancel() => MudDialog.Cancel();
    }
}