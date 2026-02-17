using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public class Person : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
