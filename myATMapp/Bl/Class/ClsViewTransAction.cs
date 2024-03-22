using myATMapp.Bl.@interface;
using myATMapp.Domain;
using myATMapp.Domain.Enums;
using myATMapp.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Bl.Class
{
    public static class ClsViewTransAction
    {
        // Initialize
        private static List<TransActionModel> ListTransAction = new List<TransActionModel>();

        // method
        public static List<TransActionModel> InsertTransAction(int userbankaccountId, EnTransActionType TranType, decimal TranAmount, string desc)
        {
            // create transaction 
            var transaction = new TransActionModel()
            {
                TransActionId = ClsUiHelper.TransActionId(),
                UserBankAccountId = userbankaccountId,
                TransActionDate = DateTime.Now,
                TransActionType = TranType,
                TransActionAmount = TranAmount,
                Description = desc
            };

            // add transaction object to the list
            ListTransAction.Add(transaction);

            // return
            return ListTransAction;
        }

    }
}
