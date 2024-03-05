using myATMapp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Ui
{
    public static class ClsValidator
    {
        /// <summary>
        /// this method convert data type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Prompt"></param>
        /// <returns></returns>
        public static T convert<T>(string Prompt)
        {
            bool IsValide = false;
            string sUserInput = string.Empty;

            // loop
            while (!IsValide)
            {
                sUserInput = ClsUiHelper.GetUserInput(Prompt);

                // try and cache
                try
                {
                    var vConverter = TypeDescriptor.GetConverter(typeof(T));

                    // validate
                    if (vConverter != null)
                        return (T)vConverter.ConvertFromString(sUserInput);
                    else
                        return default;
                }
                catch
                {
                    ClsUiHelper.PrintMessage("Invalid Input . Trye again", false);
                }
            }
            return default;
        }

        /// <summary>
        /// this method limit at number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool limit(int number)
        {
            //

            int nConvert = 0;
            // validate
            if ((number.ToString().Length < 6) || (number.ToString().Length > 6))
            {
                ClsUiHelper.PrintMessage("number must be six , Trye again", false);
                return false;
            }
            else
            {
                nConvert = number;
                return true;
            }

            // return number
            return default;

        }
        /// <summary>
        /// this method get secret input
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public static string GetSecretInput(string prompt)
        {
            // initialize
            bool IsPrompt = true;
            //
            StringBuilder input = new StringBuilder();

            // while loop
            while (true)
            {
                // condition
                if (IsPrompt)
                    Console.WriteLine(prompt);
                IsPrompt = false;

                // key
                ConsoleKeyInfo InputKay = Console.ReadKey(true);

                // validate input kye == enter kay
                if (InputKay.Key == ConsoleKey.Enter)
                {
                    if (!(input.Length == 6))
                    {
                        ClsUiHelper.PrintMessage("\n Please Enter 6 digits.", false);
                        // clear input
                        input.Clear();
                        IsPrompt = true;
                        continue;
                    }
                    else
                        break;
                }

                // validate input kye == backspace kay 
                if (InputKay.Key == ConsoleKey.Backspace && input.Length > 0)
                    input.Remove(input.Length - 1, 1);
                else if (InputKay.Key != ConsoleKey.Backspace)
                {
                    input.Append(InputKay.KeyChar);
                    Console.Write($" * ");
                }
            }
            // return
            return input.ToString();
        }

        // this method check card number
        public static int CheckCardNumber()
        {
            // Variable
            bool IsValide = false;
            int nNumberCard = 0;

            // loop a Invalid
            while (!IsValide)
            {
                nNumberCard = ClsValidator.convert<int>("your Card Number");
                // Check 
                IsValide = !ClsValidator.limit(nNumberCard) ? false : true;
            }


            // return
            return nNumberCard;

        }

        // this method check card pin
        public static int CheckCardPIN()
        {
            int nPIN = Convert.ToInt32(ClsValidator.GetSecretInput("Enter your Card Pin"));
            return nPIN;
        }

    }
}

