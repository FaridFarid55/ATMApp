using myATMapp.App;
using myATMapp.Bl.Class;
using myATMapp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Ui
{
    public class ClsAppScreen
    {
        internal const string cur = "N";

        internal static void Welcome()
        {
            // clears the console screen
            Console.Clear();

            // sets the title of the console windows
            Console.Title = "My ATM App";

            // sets the text color foreground color
            Console.ForegroundColor = ConsoleColor.White;

            //set the welcome message
            Console.WriteLine("\n\n--------------- Welcome to My ATM App ---------------\n");

            // prompt the user to insert atm card
            Console.WriteLine("Please Insert your ATM Card");
            Console.WriteLine("Note: Actual ATM Machin Will Accept" +
                " And Validate a Physical ATM Card, Read the Card Number And Validate it. ");

            ClsUiHelper.PressEnterToContinue();
        }

        internal static UserAccountActionModel UserLoginForm()
        {
            UserAccountActionModel TempUserAccount = new UserAccountActionModel();

            // this method check card number
            TempUserAccount.CardNumber = ClsValidator.CheckCardNumber();

            // this method check pin  password
            TempUserAccount.CardPin = ClsValidator.CheckCardPIN();

            TempUserAccount.Id = ClsUiHelper.TransActionId();

            // return
            return TempUserAccount;
        }

        /// <summary>
        /// this method login progers
        /// </summary>
        internal static void LoginProgers()
        {
            Console.WriteLine("\nChecking Card Number And PIN....\n");

            // get print do animation
            ClsUiHelper.PrintDoAntimine();
        }

        /// <summary>
        ///  this method Print Lock Screen
        /// </summary>
        internal static void PrintLockScreen()
        {
            Console.Clear();
            ClsUiHelper.PrintMessage("your Account is Locked. Please go to the nearest branch" +
                                    "to unLock your Account. Thank you.", true);

            // Exit program
            Environment.Exit(1);
        }

        /// <summary>
        /// this method welcome to back ..app
        /// </summary>
        internal static void WelcomeCustomer(string FullName)
        {
            Console.WriteLine($"Welcome to Back : {FullName}");
            ClsUiHelper.PressEnterToContinue();
        }

        /// <summary>
        /// this method display app menu
        /// </summary>
        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine(" ----------- My ATM App Menu --------------- ");
            Console.WriteLine(":                                           :");
            Console.WriteLine(":1. Account Balance                         :");
            Console.WriteLine(":2. Cash Deposit                            :");
            Console.WriteLine(":3. Withdrawal                              :");
            Console.WriteLine(":4. TransFer                                :");
            Console.WriteLine(":5. ViewTransAction                         :");
            Console.WriteLine(":0. Logout                                  :\n");
        }

        /// <summary>
        /// this method logout progress
        /// </summary>
        internal static void LogoutProgress()
        {
            Console.WriteLine("Thank you for Using My ATM App.");
            ClsUiHelper.PrintDoAntimine();
            Console.Clear();
        }

        /// <summary>
        /// this method main Select Amount
        /// </summary>
        /// <returns></returns>
        internal static decimal SelectAmount()
        {
            Console.WriteLine("");
            Console.WriteLine("1. {0}500                 5. {0}5000\n" +
                              "2. {0}1000                6. {0}10,000\n" +
                              "3. {0}1500                7. {0}15,000\n" +
                              "4. {0}2000                8. {0}20,000\n" +
                                         "0. {0}other", cur);
            Console.WriteLine("");
            //
            decimal oSelectAmount = ClsValidator.convert<decimal>("option:");

            // return option Make Withdrawal
            return ClsUiHelper.OptionWithdrawal(oSelectAmount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static ClsInternalTransFer internalTransFerFrom()
        {
            var oInternalTransFer = new ClsInternalTransFer();
            oInternalTransFer.RecipientBankAccountNumber = ClsValidator.convert<int>("Recipient`s Account Numbers");
            oInternalTransFer.TransFerAmount = ClsValidator.convert<decimal>($"Amount {cur}");
            oInternalTransFer.RecipientBankAccountName = ClsUiHelper.GetUserInput("Recipient`s Name :");

            // return
            return oInternalTransFer;
        }


        /// <summary>
        /// this method Pre View Bank Notes
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        internal static bool PreViewBankNotes(int amount)
        {
            decimal nThousAndNotesCount = amount / 1000;
            decimal nFiveHundredAndNotesCount = (amount % 1000) / 500;

            Console.WriteLine("\n Summary");
            Console.WriteLine("----------");
            Console.WriteLine($"{ClsAppScreen.cur} : 1000 X {nThousAndNotesCount} = {1000 * nThousAndNotesCount}");
            Console.WriteLine($"{ClsAppScreen.cur} : 500  X {nFiveHundredAndNotesCount} = {500 * nFiveHundredAndNotesCount}");
            Console.WriteLine($"a Total Amount {ClsUiHelper.FormatAmount(amount)}\n\n");

            byte nPotion = ClsValidator.convert<byte>("1 to confirm");

            // return
            return nPotion.Equals(1);
        }
    }
}
