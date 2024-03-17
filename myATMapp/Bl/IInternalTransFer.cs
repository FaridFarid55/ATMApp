using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Bl
{
    public interface IInternalTransFer
    {
        public decimal TransFerAmount { get; set; }
        public string RecipientBankAccountName { get; set; }
        public int RecipientBankAccountNumber { get; set; }
    }
}
