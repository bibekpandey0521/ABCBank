namespace Common.Requests
{
    public class CreateAccountHolder
    {
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }
    //public record UpdateAccountHolder(int Id,string FirstName, string LastName,
    //    DateTime dateOfBirth, string ContactNumber, string Email);


    public class UpdateAccountHolder
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       /// <summary>
       /// public DateTime? DateOfBirth { get; set; }
       /// </summary>
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }
}
