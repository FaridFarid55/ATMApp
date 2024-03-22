using myATMapp.Bl.@interface;
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
    public class ClsPlaceDeposit : IUserAccountAction
    {
        // property
        public List<TransActionModel> ListTransAction { get; set; }

        // Constrictor
        public ClsPlaceDeposit()
        {
            ListTransAction = new List<TransActionModel>();
        }

        // method 
        public void PlaceDeposit(UserAccountActionModel userAccounts)
        {
            Console.WriteLine("\n only multiples of 500 and 1000 naira  allowed.\n");
            int nTransitionAmt = ClsValidator.convert<int>($"amount {ClsAppScreen.cur}");

            // simulate counting
            Console.WriteLine("\n Checking And Counting bank notes.\n");
            ClsUiHelper.PrintDoAntimine();
            Console.WriteLine("");

            // some bad clothes
            ClsUiHelper.CheckAmount(nTransitionAmt);

            // check
            ClsUiHelper.EqualsAmount(nTransitionAmt, userAccounts.AccountBalance, "PlaceDeposit failed. you do not have enough balance");

            // check as confirm
            if (ClsAppScreen.PreViewBankNotes(nTransitionAmt) == false)
            {
                ClsUiHelper.PrintMessage("you Have Cancelled your action.", false);
                return;
            }

            // bind transaction details to transaction object
            ListTransAction = ClsViewTransAction.InsertTransAction(userAccounts.Id, EnTransActionType.Deposit, nTransitionAmt, "");

            // update account balance
            userAccounts.AccountBalance += nTransitionAmt;

            // print success message 
            ClsUiHelper.PrintMessage($"your deposit of : {ClsUiHelper.FormatAmount(nTransitionAmt)} was" +
                $" successfully.", true);

        }
    }
}
