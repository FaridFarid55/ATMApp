using myATMapp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Bl
{
    public interface IUserAccountAction
    {
        void CheckBalance();
        void PlaceDeposit();
        void MakeWithdrawal();
    }
}
