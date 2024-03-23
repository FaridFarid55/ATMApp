using myATMapp.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Sirelze
{
    public class SqlSirelzeATM : ISirelzeATm
    {
        public List<UserAccountActionModel> Desirelze(string Jsoin)
        {
            List<UserAccountActionModel> ATmModels = JsonConvert.DeserializeObject<List<UserAccountActionModel>>(Jsoin);
            return ATmModels;
        }

        public string Sirelze(List<UserAccountActionModel> UserAccount)
        {
            string File = JsonConvert.SerializeObject(UserAccount);
            return File;
        }

        public string Sirelze(DataTable Table)
        {
            string File = JsonConvert.SerializeObject(Table);
            return File;
        }
    }
}
