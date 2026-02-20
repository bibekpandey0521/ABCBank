namespace Common.Requests
{
    public record CreateAccountHolder(string FirstName, string LastName,
        DateTime dateOfBirth, string ContactNumber, string Email);
    public record UpdateAccountHolder(int Id,string FirstName, string LastName,
        DateTime dateOfBirth, string ContactNumber, string Email);
}
