using ConsoleTables;
using myATMapp.Bl.Class;
using myATMapp.Bl.@interface;
using myATMapp.Domain;
using myATMapp.Domain.Enums;
using myATMapp.Domain.Models;
using myATMapp.Dal;
using myATMapp.Ui;
using static System.Runtime.InteropServices.JavaScript.JSType;
using myATMapp.Sirelze;

namespace myATMapp.App
{
    class ATMApp : IUserLogin
    {
        #region Initialize
        // Initialize
        private IReadeATM _oReadATM;
        private ISirelzeATm _oSirelzeATM;
        IUserAccountAction user = new ClsMakeWithdrawal();
        private UserAccountActionModel UserAccount = new UserAccountActionModel();
        private List<TransActionModel> ListTransAction;
        #endregion

        // Constrctor
        public ATMApp(IReadeATM oReadATM, ISirelzeATm oSirelzeATM)
        {
            _oReadATM = oReadATM;
            _oSirelzeATM = oSirelzeATM;
        }

        /// <summary>
        /// this method  Initialize data
        /// </summary>
        /// <returns></returns>
        public List<UserAccountActionModel> Initialize()
        {
            // open file
            string File = _oReadATM.ReadeATM();

            // Desirelze json Object
            List<UserAccountActionModel> ListUserAccount = _oSirelzeATM.Desirelze(File);

            // return
            return ListUserAccount;

        }

        /// <summary>
        /// this method Run program
        /// </summary>
        public void Run(bool IsCheck = true)
        {
            if (IsCheck == false)
            {
                ClsUiHelper.PrintMessage("Exiting program...", false);
                Environment.Exit(0);
            }
            // get  Welcome App!
            ClsAppScreen.Welcome();

            // get  Check User Card Number And number PIN
            CheckUserCardNumberAndCardPIN();

            // get welcome customer
            ClsAppScreen.WelcomeCustomer(UserAccount.FullName);
            while (IsCheck)
            {
                // IsCheck = ClsUiHelper.CheckProgram();
                // get display Menu
                ClsAppScreen.DisplayAppMenu();

                // get Menu Option
                ProcessMenuOption();
            }
        }

        /// <summary>
        /// this method Check User Card Number And Check Number PIN and pass
        /// </summary>
        public void CheckUserCardNumberAndCardPIN()
        {
            bool IsCorrectLogin = false;

            // loop
            while (IsCorrectLogin == false)
            {
                var x = 0;
                // get User Login Form
                UserAccountActionModel InputAccount = ClsAppScreen.UserLoginForm();

                // get login progers
                ClsAppScreen.LoginProgers();

                // loop
                foreach (UserAccountActionModel account in Initialize())
                {
                    UserAccount = account;

                    // check card number  and number PIN
                    if (InputAccount.CardNumber.Equals(UserAccount.CardNumber) && InputAccount.CardPin.Equals(UserAccount.CardPin))
                    {
                        UserAccount.TotalLogin++;
                        //
                        UserAccount = account;
                        //
                        if (UserAccount.IsLocked || UserAccount.TotalLogin > 3)
                            ClsAppScreen.PrintLockScreen();
                        else
                        {
                            UserAccount.TotalLogin = 0;
                            IsCorrectLogin = true;
                            break;
                        }
                    }
                }

                // check Card number ot Number PIN = false
                if (IsCorrectLogin == false)
                {
                    ClsUiHelper.PrintMessage("\nInvalid card number or PIN.", false);
                    UserAccount.IsLocked = UserAccount.TotalLogin == 3;
                    if (UserAccount.IsLocked)
                        ClsAppScreen.PrintLockScreen();
                    // Clear
                    Console.Clear();
                }
            }

        }

        /// <summary>
        ///  get menu option
        /// </summary>
        private void ProcessMenuOption()
        {
            switch (ClsValidator.convert<int>("an option\n"))
            {
                // Check Balance
                case (int)EnAppMenu.CheckBalance:
                    ClsCheckBalance Check = new ClsCheckBalance();
                    Check.CheckBalance(UserAccount);
                    break;

                // Place Deposit
                case (int)EnAppMenu.PlaceDeposit:
                    ClsPlaceDeposit placeDeposit = new ClsPlaceDeposit();
                    placeDeposit.PlaceDeposit(UserAccount);
                    break;

                // MakeWithdrawal
                case (int)EnAppMenu.MakeWithdrawal:
                    ClsMakeWithdrawal clsMake = new ClsMakeWithdrawal();
                    clsMake.MakeWithdrawal(UserAccount, out ListTransAction);
                    break;
                // InternalTransFer
                case (int)EnAppMenu.InternalTransFer:
                    var ternsFer = ClsAppScreen.internalTransFerFrom();
                    ClsInternalTransFer internalTransFer = new ClsInternalTransFer();
                    internalTransFer.ProcessInternalTranFer(ternsFer, UserAccount);
                    break;
                // ViewTransAction
                case (int)EnAppMenu.ViewTransAction:
                    ViewTransAction(ListTransAction);
                    break;

                // Logout
                case (int)EnAppMenu.Logout:
                    ClsAppScreen.LogoutProgress();
                    ClsUiHelper.PrintMessage("you have successfully logged out .Please Collect" +
                        "your ATM Card.", true);

                    // return
                    Run(ClsUiHelper.CheckProgram());
                    break;
                default:
                    Console.Clear();
                    ClsUiHelper.PrintMessage("Invalid option.....", false);
                    break;
            }
        }

        // method
        /// <summary>
        /// this method View TransAction
        /// </summary>
        /// <param name="listtransaction"></param>
        public void ViewTransAction(List<TransActionModel> listtransaction)
        {

            // ListTransAction =oClsViewTransAction.InsertTransAction()
            var FlitedTransActionList = listtransaction.Where(a => a.UserBankAccountId == UserAccount.Id).ToList();

            // Check there`s transaction
            if (FlitedTransActionList.Count <= 0)
            {
                ClsUiHelper.PrintMessage("you have no transaction yet.", true);
            }
            else
            {
                var table = new ConsoleTable("Id", "TransAction Data", "Type", "Description", "Amount" + ClsAppScreen.cur);
                foreach (var t in FlitedTransActionList)
                {
                    table.AddRow(t.TransActionId, t.TransActionDate, t.TransActionType, t.Description, t.TransActionAmount);
                }
                table.Options.EnableCount = false;
                table.Write();
                ClsUiHelper.PrintMessage($"you have : {FlitedTransActionList.Count} TransAction(s).", true);
            }
        }
    }
}


