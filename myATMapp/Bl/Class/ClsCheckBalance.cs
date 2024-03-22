using myATMapp.Bl.@interface;
using myATMapp.Domain;
using myATMapp.Domain.Models;
using myATMapp.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Bl.Class
{
    public class ClsCheckBalance : IUserAccountAction
    {
        // property
        public List<TransActionModel> ListTransAction { get; set; }

        // Constrictor
        public ClsCheckBalance()
        {
            ListTransAction = new List<TransActionModel>();
        }

        // method 
        public void CheckBalance(UserAccountActionModel useraccount)
        {
            ClsUiHelper.PrintMessage($"your Account balance is : {ClsUiHelper.FormatAmount(useraccount.AccountBalance)}");
        }
    }
}
