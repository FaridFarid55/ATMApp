using myATMapp.Bl.Class;
using myATMapp.Bl.@interface;
using myATMapp.Domain.Models;
using myATMapp.Ui;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize
            UserAccountActionModel oUserAccount = new UserAccountActionModel();
            ATMApp oATMApp = new ATMApp();

            // run
            oATMApp.Run();
            Console.WriteLine();
        }

    }
}
