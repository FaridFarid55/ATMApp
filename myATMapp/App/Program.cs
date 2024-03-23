using myATMapp.Dal;
using myATMapp.Sirelze;

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
