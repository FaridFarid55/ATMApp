using myATMapp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Domain
{
    public class TransActionModel
    {
        // property
        public int TransActionId { get; set; }
        public int UserBankAccountId { get; set; }
        public DateTime TransActionDate { get; set; }
        public EnTransActionType TransActionType { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal TransActionAmount { get; set; }

    }
}
