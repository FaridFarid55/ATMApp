using myATMapp.Bl.@interface;
using myATMapp.Dal;
using myATMapp.Domain;
using myATMapp.Domain.Enums;
using myATMapp.Domain.Models;
using myATMapp.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Bl.Class
{
    public class ClsMakeWithdrawal : IUserAccountAction
    {
        // proports
        public List<TransActionModel> ListTransAction { get; set; }

        //Constrictor
        public ClsMakeWithdrawal()
        {
            ListTransAction = new List<TransActionModel>();
        }

        // method
        public void MakeWithdrawal(UserAccountActionModel useraccount, out List<TransActionModel> x)
        {
            decimal cSelectAmount = ClsAppScreen.SelectAmount();
            // condition
            if (cSelectAmount == -1)
            {
                MakeWithdrawal(useraccount, out x);
                return;
            }
            else if (cSelectAmount != 0)
                cSelectAmount = cSelectAmount;
            else
                cSelectAmount = ClsValidator.convert<decimal>($"Amount {ClsAppScreen.cur}");

            // input validate
            ClsUiHelper.CheckAmount(cSelectAmount);

            //business logic validation
            ClsUiHelper.EqualsAmount(cSelectAmount, useraccount.AccountBalance, "Withdrawal failed . your balance is to low to Withdrawal");

            // view Withdrawal deities transaction object
            ListTransAction = ClsViewTransAction.InsertTransAction(useraccount.Id, EnTransActionType.Withdrawal, cSelectAmount, "");
            x = ListTransAction;
            // update account balance
            useraccount.AccountBalance -= cSelectAmount;

            // print success message 
            ClsUiHelper.PrintMessage($"your have successfully Withdrawal" +
                $" : {ClsUiHelper.FormatAmount(cSelectAmount)}", true);
        }
    }
}
