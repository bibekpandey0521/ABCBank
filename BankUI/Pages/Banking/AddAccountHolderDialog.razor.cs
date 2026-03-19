using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Runtime.CompilerServices;

namespace BankUI.Pages.Banking
{
    public partial class AddAccountHolderDialog
    {

        [Parameter] public CreateAccountHolder CreateAccountHolderRequest { get; set; }

        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        private void Submit() => MudDialog.Close(DialogResult.Ok(true));

        MudForm _form = default;

        private async  Task SaveAsync()
        {
            MudDialog.Close(DialogResult.Ok(true));
        }

        void Cancel() => MudDialog.Cancel();
    }
}