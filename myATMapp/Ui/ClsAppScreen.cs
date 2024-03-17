using myATMapp.App;
using myATMapp.Bl;
using myATMapp.Domain;
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

        internal static UserAccount UserLoginForm()
        {
            UserAccount TempUserAccount = new UserAccount();

            // this method check card number
            TempUserAccount.CardNumber = ClsValidator.CheckCardNumber();

            // this method check pin  password
            TempUserAccount.CardPin = ClsValidator.CheckCardPIN();

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
            ClsUiHelper.PressEnterToContinue();

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
            Console.WriteLine("------------ My ATM App Menu ------------");
            Console.WriteLine(":                                       :");
            Console.WriteLine(":1. Account Balance                     :");
            Console.WriteLine(":2. Cash Deposit                        :");
            Console.WriteLine(":3. Withdrawal                          :");
            Console.WriteLine(":4. TransFer                            :");
            Console.WriteLine(":5. TransAction                         :");
            Console.WriteLine(":5. Logout                              :\n");
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
        internal static float SelectAmount()
        {
            Console.WriteLine("");
            Console.WriteLine("1. {0}500                 5. {0}5000\n" +
                              "2. {0}1000                6. {0}10,000\n" +
                              "3. {0}1500                7. {0}15,000\n" +
                              "4. {0}2000                8. {0}20,000\n" +
                                         "0. {0}other", cur);
            Console.WriteLine("");
            //
            float oSelectAmount = ClsValidator.convert<float>("option:");

            // return option Make Withdrawal
            return ClsUiHelper.OptionWithdrawal(oSelectAmount);
        }

        internal static IInternalTransFer internalTransFerFrom()
        {
            var oInternalTransFer = new IInternalTransFer();
            oInternalTransFer.RecipientBankAccountNumber = ClsValidator.convert<int>("Recipient`s Account Numbers");
            oInternalTransFer.TransFerAmount = ClsValidator.convert<decimal>($"Amount {cur}");
            oInternalTransFer.RecipientBankAccountName = ClsUiHelper.GetUserInput("Recipient`s Name :");

            // retuen
            return oInternalTransFer;
        }
    }
}
