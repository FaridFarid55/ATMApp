using myATMapp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Bl
{
    public interface ITransAction
    {
        void InsertTransAction(int userbankaccount, EnTransActionType TranType, decimal TranAmount, string desc);
        void ViewTransAction();
    }
}
