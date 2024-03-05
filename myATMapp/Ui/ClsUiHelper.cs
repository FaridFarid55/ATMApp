using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Ui
{
    public static class ClsUiHelper
    {
        private static int TranId;
        private static CultureInfo Cultural = new CultureInfo("IG-NG");

        // get print message
        public static void PrintMessage(string message, bool success = true)
        {
            if (success)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            //
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            //
            PressEnterToContinue();
        }

        // get user input
        public static string GetUserInput(string input)
        {
            Console.WriteLine($"Please Enter {input}");
            return Console.ReadLine();
        }

        // get print do antimine
        public static void PrintDoAntimine(int timer = 15)
        {
            // condition
            for (int i = 0; i < timer; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
            // clear
            Console.Clear();
        }

        // press enter to continuo
        public static void PressEnterToContinue()
        {
            Console.WriteLine("\n\nPress Enter to Continue.......\n");
            Console.ReadLine();
        }

        // set timer
        public static void Timer()
        {
            int nTimer = 15;
            Console.WriteLine("\n\nChecking Card Number and Number PIN");

            // loop
            for (int i = 0; i < nTimer; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            Console.Clear();
        }

        // set format decimal
        public static string FormatAmount(decimal balance)
        {
            return String.Format(Cultural, "{0:C2}", balance);
        }

        // set transaction id 
        public static int TransActionId()
        {
            return ++TranId;
        }
    }
}
