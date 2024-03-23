using myATMapp.Dal;
using myATMapp.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Dal
{
    public class JsonFileDl : IReadeATM
    {
        public string ReadeATM()
        {
            // Opne Json File 
            var file = File.ReadAllText("ATMjson22.json");

            // Check File Exists
            if (File.Exists(file))
            {
                ClsUiHelper.PrintMessage("File not Exists", false);
                return "";
            }

            // Check File Null Or Empty
            if (string.IsNullOrEmpty(file))
            {
                ClsUiHelper.PrintMessage("File Is Empty", false);
                return "";
            }
            //
            return file;
        }
    }
}
