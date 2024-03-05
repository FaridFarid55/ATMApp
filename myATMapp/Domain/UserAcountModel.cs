using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Domain
{
    public class UserAccount
    {
        // property
        public int Id { get; set; }
        public string FullName { get; set; }
        public int CardNumber { get; set; }
        public int CardPin { get; set; }
        public int AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public int TotalLogin { get; set; }
        public bool IsLocked { get; set; }


    }
}
