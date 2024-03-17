using myATMapp.Domain;
using myATMapp.Ui;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.App
{
    public class Entry
    {
        public static void Main(string[] args)
        {
            UserAccount oUserAccount = new UserAccount();
            ATMApp oATMApp = new ATMApp();

            // get Initialize Date
            oATMApp.InitializeDate();

            // run
            oATMApp.Run();
            Console.WriteLine();

        }

    }
}
