using Common.Requests;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace BankUI.Pages.Banking.Validators
{
    public class CreateAccountHolderValidator : AbstractValidator<CreateAccountHolder>
    {
        public CreateAccountHolderValidator()
        {
            RuleFor(accountHolder => accountHolder.FirstName)
                .Must(f => !string.IsNullOrEmpty(f))
                .WithMessage("FirstName is required!");

            RuleFor(accountHolder => accountHolder.LastName)
                .Must(f => !string.IsNullOrEmpty(f))
                .WithMessage("Last Name is required!");

            RuleFor(accountHolder => accountHolder.DateOfBirth)
                .NotEmpty()
                .WithMessage("Date of Birth is required!")
                .LessThanOrEqualTo(DateTime.Now.AddYears(-16))
                .WithMessage("Account Holder should be 16 year or older.");

            RuleFor(accountHolder => accountHolder.ContactNumber)
                .Must(f => !string.IsNullOrEmpty(f))
                .WithMessage("Contact number  is required")
                .Matches(@"(^\d{12})")
                .WithMessage("Only 12 digits are allowed.");

            RuleFor(accountHolder => accountHolder.Email)
                .Must(f => !string.IsNullOrEmpty(f))
                .EmailAddress()
                .MaximumLength(20);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (requestModel, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateAccountHolder>
                .CreateWithOptions((CreateAccountHolder)requestModel, x=> x.IncludeProperties(propertyName)));
            if (result.IsValid)
            {
                return Array.Empty<string>();
            }
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
