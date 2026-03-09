using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Responses
{
    public class TransactionResponse
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public AccountResponse Account { get; set; }
    }
}
