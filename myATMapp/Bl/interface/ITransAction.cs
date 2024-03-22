using myATMapp.Domain;
using myATMapp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Bl.@interface
{
    public interface ITransAction
    {
        // method
         List<TransActionModel> InsertTransAction(int userbankaccountId, EnTransActionType TranType, decimal TranAmount, string desc);
    }
}
