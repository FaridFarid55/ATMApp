using myATMapp.Bl.Class;
using myATMapp.Bl.@interface;
using myATMapp.Dal;
using myATMapp.Domain.Models;
using myATMapp.Sirelze;
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
            var Sirelze = new SqlSirelzeATM();
            ATMApp oATMApp = new ATMApp(new SqlServerATM(Sirelze), Sirelze);

            // run
            oATMApp.Run();
            Console.WriteLine();
        }

    }
}
