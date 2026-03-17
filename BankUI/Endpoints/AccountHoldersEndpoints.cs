namespace BankUI.Endpoints
{
    public static class AccountHoldersEndpoints
    {
        public const string Add = "api/accountHolders/add";
        public const string Update = "api/accountHolders/update";
        public const string Delete = "api/accountHolders";
        public const string GetAll = "api/accountHolders/all";
        public static string GetById(int id)
        {
            return $"api/accountholders/{id}";
        }
    }
}
