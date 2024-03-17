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
        private const float cMinKeptAmount = 500;

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
                    MakeWithdrawal();
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
            int nTransitionAmt = ClsValidator.convert<int>($"amount {ClsAppScreen.cur}");

            // simulate counting
            Console.WriteLine("\n Checking And Counting bank notes.\n");
            ClsUiHelper.PrintDoAntimine();
            Console.WriteLine("");

            // some bad clothes
            if (nTransitionAmt <= 0)
            {
                ClsUiHelper.PrintMessage("Amount needs to be greater than zero. Try again.", false);
                return;
            }

            if (nTransitionAmt % 500 != 0)
            {
                ClsUiHelper.PrintMessage("Enter Deposit Amount in multiples of 500 or 1000. Try again.", false);
                return;
            }
            if (PreViewBankNotes(nTransitionAmt) == false)
            {
                ClsUiHelper.PrintMessage("you Have Cancelled your action.", false);
                return;
            }

            // bind transaction details to transaction object
            InsertTransAction(SelectedAccount.Id, EnTransActionType.Deposit, nTransitionAmt, "");

            // update account balance
            SelectedAccount.AccountBalance += nTransitionAmt;

            // print success message 
            ClsUiHelper.PrintMessage($"your deposit of : {ClsUiHelper.FormatAmount(nTransitionAmt)} was" +
                $" successfully.", true);

        }


        public void MakeWithdrawal()
        {
            float cTransActionAmt = 0;
            float cSelectAmount = ClsAppScreen.SelectAmount();

            // condition
            if (cSelectAmount == -1)
                cSelectAmount = ClsAppScreen.SelectAmount();
            else if (cSelectAmount != 0)
                cTransActionAmt = cSelectAmount;
            else
                cTransActionAmt = ClsValidator.convert<float>($"Amount {ClsAppScreen.cur}");

            // input validate
            if (cTransActionAmt <= 0)
            {
                ClsUiHelper.PrintMessage("Amount needs to be greater than zero. Tray again", false);
                return;
            }
            if (cTransActionAmt % cMinKeptAmount != 0)
            {
                ClsUiHelper.PrintMessage("you can only Withdrawal in Amount multipoles of 500 or 1000 naira. Try again", false);
                return;
            }

            //business logic validation
            if (cTransActionAmt > (float)SelectedAccount.AccountBalance)
            {
                ClsUiHelper.PrintMessage($"Withdrawal failed . your balance is to low to Withdrawal" +
                    $"{ClsUiHelper.FormatAmount((decimal)cTransActionAmt)}", false);
                return;
            }

            if ((float)SelectedAccount.AccountBalance - cMinKeptAmount < cTransActionAmt)
            {
                ClsUiHelper.PrintMessage($"Withdrawal failed . your account needs to have " +
                    $"minimum {ClsUiHelper.FormatAmount((decimal)cMinKeptAmount)}");
                return;
            }

            // bind Withdrawal deities transaction object
            InsertTransAction(SelectedAccount.Id, EnTransActionType.Withdrawal, (decimal)cTransActionAmt, "");

            // update account balance
            SelectedAccount.AccountBalance -= (decimal)cTransActionAmt;

            // print success message 
            ClsUiHelper.PrintMessage($"your have successfully Withdrawal" +
                $" : {ClsUiHelper.FormatAmount((decimal)cTransActionAmt)}", true);
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

        private void ProcessInternalTranFer(IInternalTransFer transFer)
        {
            if (transFer.TransFerAmount <= 0)
            {
                ClsUiHelper.PrintMessage("Amount needs to be more than zero. Tray again", false);
                return;
            }

            // check sender`s account balance
            if (transFer.TransFerAmount > SelectedAccount.AccountBalance)
            {
                ClsUiHelper.PrintMessage($"Transfer failed. you do not have enough balance" +
                    $"to Transfer {ClsUiHelper.FormatAmount(transFer.TransFerAmount)}", false);
                return;
            }

            // check the minimum kept amount
            if ((float)SelectedAccount.AccountBalance - cMinKeptAmount > cMinKeptAmount)
            {
                ClsUiHelper.PrintMessage($"Transfer failed. your account needs to have minimum balance" +
                $"{ClsUiHelper.FormatAmount((decimal)cMinKeptAmount)}", false);
                return;
            }

            // check Recipient`s Account Numbers valid
            var SelectedBankAccountRecipient =
                (
                 from UserAcc in UserAccountList
                 where UserAcc.AccountNumber == transFer.RecipientBankAccountNumber
                 select UserAcc
                ).FirstOrDefault();

            // check null
            if (SelectedBankAccountRecipient == null)
            {
                ClsUiHelper.PrintMessage("Transfer failed. Receiver bank account Number is invalid", false);
                return;
            }

            // Check Recipient`s  Name 
            if (SelectedBankAccountRecipient.FullName != transFer.RecipientBankAccountName)
            {
                ClsUiHelper.PrintMessage("Transfer failed. Receiver bank account Name is invalid", false);
                return;
            }
        }
    }
}


