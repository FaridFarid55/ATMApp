using myATMapp.Bl.@interface;
using myATMapp.Domain;
using myATMapp.Domain.Enums;
using myATMapp.Domain.Models;
using myATMapp.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace myATMapp.Bl.Class
{
    public class ClsInternalTransFer : IUserAccountAction
    {
        // property
        public decimal TransFerAmount { get; set; }
        public string RecipientBankAccountName { get; set; }
        public int RecipientBankAccountNumber { get; set; }
        public List<UserAccountActionModel> ListUserAccount { get; set; }
        public List<TransActionModel> ListTransAction { get; set; }

        // Constrictor
        public ClsInternalTransFer()
        {
            ListTransAction = new List<TransActionModel>();
            ListUserAccount = new List<UserAccountActionModel>();
        }

        // method
        public void ProcessInternalTranFer(ClsInternalTransFer transFer, UserAccountActionModel useraccount)
        {
            if (!ClsUiHelper.CheckAmount(transFer.TransFerAmount))
                return;

            // check sender`s UserAccount balance and the minimum kept amount
            if (!ClsUiHelper.EqualsAmount(transFer.TransFerAmount, useraccount.AccountBalance, "Transfer failed. you do not have enough balance"))
                return;

            // check Recipient`s Account Numbers valid
            var SelectedBankAccountRecipient =
                (
                 from UserAcc in ListUserAccount
                 where UserAcc.AccountNumber == transFer.RecipientBankAccountNumber
                 select UserAcc
                ).FirstOrDefault();

            // check null
            if (SelectedBankAccountRecipient == null)
            {
                ClsUiHelper.PrintMessage("Transfer failed. Receiver bank UserAccount Number is invalid", false);
                return;
            }

            // Check Recipient`s  Name 
            if (SelectedBankAccountRecipient.FullName != transFer.RecipientBankAccountName)
            {
                ClsUiHelper.PrintMessage("Transfer failed. Receiver bank UserAccount Name  dos not match.", false);
                return;
            }

            // add transaction to
            ListTransAction = ClsViewTransAction.InsertTransAction(useraccount.Id, EnTransActionType.TransFer, -transFer.TransFerAmount, "TransFer" +
                             $"to {SelectedBankAccountRecipient.AccountNumber}\n" +
                             $"{SelectedBankAccountRecipient.FullName}");

            // update sender`s UserAccount balance
            useraccount.AccountBalance -= transFer.TransFerAmount;

            // transaction Record-sender
            ListTransAction = ClsViewTransAction.InsertTransAction(useraccount.Id, EnTransActionType.TransFer, transFer.TransFerAmount, "TransFred from" +
                            $"to {SelectedBankAccountRecipient.AccountNumber}\n" +
                            $"{SelectedBankAccountRecipient.FullName}");

            // update sender`s UserAccount balance
            SelectedBankAccountRecipient.AccountBalance += transFer.TransFerAmount;

            //print Success
            ClsUiHelper.PrintMessage("\nyou have Successfully transferred : " +
                $"{transFer.TransFerAmount}" +
                $" to : {transFer.RecipientBankAccountName}", true);
        }
    }
}
