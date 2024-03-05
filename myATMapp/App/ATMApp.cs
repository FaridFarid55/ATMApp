using myATMapp.Bl;
using myATMapp.Domain;
using myATMapp.Domain.Enums;
using myATMapp.Ui;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace myATMapp.App
{
    class ATMApp : IUserLogin, IUserAccountAction, ITransAction
    {
        private List<UserAccount> UserAccountList;
        private UserAccount SelectedAccount;
        private List<TransActionModel> ListTransAction = new List<TransActionModel>();

        public void Run()
        {
            // get  Welcome App!
            ClsAppScreen.Welcome();

            // get  Check User Card Number And number PIN
            CheckUserCardNumberAndPassword();

            // get welcome customer
            ClsAppScreen.WelcomeCustomer(SelectedAccount.FullName);

            // get display Menu
            ClsAppScreen.DisplayAppMenu();

            // get Menu Option
            ProcessMenuOption();
        }

        /// <summary>
        /// this method Initialize Date account
        /// </summary>
        public void InitializeDate()
        {
            UserAccountList = new List<UserAccount>
            {
            new UserAccount
                {
                    Id=1,
                    FullName="Farid Farid",
                    CardPin=123123,
                    AccountNumber=123456,
                    CardNumber=560600,
                    AccountBalance=50000.00m,
                    IsLocked=false,
                },
            new UserAccount
                {
                    Id=2,
                    FullName="Ail ahmed",
                    AccountNumber=123457,
                    CardNumber=060600,
                    AccountBalance=40000.00m,
                    IsLocked=false,
                },
            new UserAccount
                {
                    Id=3,
                    FullName="mohamed ail",
                    CardPin=789789,
                    AccountNumber=22445566,
                    CardNumber=600600,
                    AccountBalance=20000.00m,
                    IsLocked=true,
                }
            };
        }

        /// <summary>
        /// this method Check User Card Number And Check Number PIN and pass
        /// </summary>
        public void CheckUserCardNumberAndPassword()
        {
            bool IsCorrectLogin = false;

            // loop
            while (IsCorrectLogin == false)
            {
                // get User Login Form
                UserAccount InputAccount = ClsAppScreen.UserLoginForm();

                // get login progers
                ClsAppScreen.LoginProgers();

                // loop
                foreach (UserAccount account in UserAccountList)
                {
                    SelectedAccount = account;

                    // check card number 
                    if (InputAccount.CardNumber.Equals(SelectedAccount.CardNumber))
                    {
                        SelectedAccount.TotalLogin++;

                        // check  number PIN
                        if (InputAccount.CardPin.Equals(SelectedAccount.CardPin))
                        {
                            SelectedAccount = account;

                            if (SelectedAccount.IsLocked || SelectedAccount.TotalLogin > 3)
                                ClsAppScreen.PrintLockScreen();
                            else
                            {
                                SelectedAccount.TotalLogin = 0;
                                IsCorrectLogin = true;
                                break;
                            }
                        }
                    }

                    if (IsCorrectLogin == false)
                    {
                        ClsUiHelper.PrintMessage("\nInvalid card number or PIN.", false);
                        SelectedAccount.IsLocked = SelectedAccount.TotalLogin == 3;
                        if (SelectedAccount.IsLocked)
                        {
                            ClsAppScreen.PrintLockScreen();
                        }
                    }
                    Console.Clear();
                }
            }

        }

        /// <summary>
        ///  set menu option
        /// </summary>
        private void ProcessMenuOption()
        {
            switch (ClsValidator.convert<int>("an option\n"))
            {
                case (int)EnAppMenu.CheckBalance:
                    CheckBalance();
                    break;

                case (int)EnAppMenu.PlaceDeposit:
                   PlaceDeposit();
                    break;

                case (int)EnAppMenu.MakeWithdrawal:
                    Console.WriteLine("Making Withdrawal.....\n");
                    break;

                case (int)EnAppMenu.InternalTransFer:
                    Console.WriteLine("Making Internal TransFer.....\n");
                    break;

                case (int)EnAppMenu.ViewTransAction:
                    Console.WriteLine("View TransActions.....\n");
                    break;

                case (int)EnAppMenu.Logout:
                    ClsAppScreen.LogoutProgress();
                    ClsUiHelper.PrintMessage("you have successfully logged out .Please Collect" +
                        "your ATM Card.", true);

                    // return
                    Run();
                    break;
                default:
                    ClsUiHelper.PrintMessage("Invalid option.....", false);
                    break;
            }
        }

        // method

        public void InsertTransAction(int userbankaccount, EnTransActionType TranType, decimal TranAmount, string desc)
        {
            // create transaction 
            var transaction = new TransActionModel()
            {
                TransActionId = ClsUiHelper.TransActionId(),
                UserBankAccountId = userbankaccount,
                TransActionDate = DateTime.Now,
                TransActionType = TranType,
                TransActionAmount = TranAmount,
                Description = desc
            };

            // add transaction object to the list
            ListTransAction.Add(transaction);

        }

        public void ViewTransAction()
        {
            throw new NotImplementedException();
        }

        //
        public void CheckBalance()
        {
            ClsUiHelper.PrintMessage($"your Account balance is : {ClsUiHelper.FormatAmount(SelectedAccount.AccountBalance)}");
        }

        public void PlaceDeposit()
        {
            Console.WriteLine("\n only multiples of 500 and 1000 naira  allowed.\n");
            int nTransitionAtm = ClsValidator.convert<int>($"amount {ClsAppScreen.cur}");

            // simulate counting
            Console.WriteLine("\n Checking And Counting bank notes.\n");
            ClsUiHelper.PrintDoAntimine();
            Console.WriteLine("");

            // some bad clothes
            if (nTransitionAtm <= 0)
            {
                ClsUiHelper.PrintMessage("Amount needs to be greater than zero. Try again.", false);
                return;
            }

            if (nTransitionAtm % 500 != 0)
            {
                ClsUiHelper.PrintMessage("Enter Deposit Amount in multiples of 500 or 1000. Try again.", false);
                return;
            }
            if (PreViewBankNotes(nTransitionAtm) == false)
            {
                ClsUiHelper.PrintMessage("you Have Cancelled your action.", false);
                return;
            }

            // bind transaction details to transaction object
            InsertTransAction(SelectedAccount.Id, EnTransActionType.Deposit, nTransitionAtm, "");

            // update account balance
            SelectedAccount.AccountBalance += nTransitionAtm;

            // print success message 
            ClsUiHelper.PrintMessage($"your deposit of : {ClsUiHelper.FormatAmount(nTransitionAtm)} was" +
                $" successfully.",true);
        }


        public void MakeWithdrawal()
        {
            throw new NotImplementedException();
        }

        private bool PreViewBankNotes(int amount)
        {
            int nThousAndNotesCount = amount / 1000;
            int nFiveHundredAndNotesCount = (amount % 1000) / 500;

            Console.WriteLine("\n Summary");
            Console.WriteLine("----------");
            Console.WriteLine($"{ClsAppScreen.cur} : 1000 X {nThousAndNotesCount} = {1000 * nThousAndNotesCount}");
            Console.WriteLine($"{ClsAppScreen.cur} : 500  X {nFiveHundredAndNotesCount} = {500 * nThousAndNotesCount}");
            Console.WriteLine($"a Total Amount {ClsUiHelper.FormatAmount(amount)}\n\n");

            int nPotion = ClsValidator.convert<int>("1 to confirm");

            // return
            return nPotion.Equals(1);
        }
    }
}


