using Common.Enums;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Account : BaseEntity<int>
    {
        public string AccountNumber { get; set; }
        
        public int AccountHolderId { get; set; }

        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        // Type - Enum 
        public AccountType Type { get; set; }

        public AccountHolder AccountHolder { get; set; }

        public List<Transaction>  Transactions { get; set; }
    }
}
