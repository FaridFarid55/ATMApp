
using myATMapp.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Sirelze
{
    public class JsoneSirelzeATM : ISirelzeATm
    {
        public List<UserAccountActionModel> Desirelze(string Jsoin)
        {
            List<UserAccountActionModel> ATmModels = JsonConvert.DeserializeObject<List<UserAccountActionModel>>(Jsoin);
            return ATmModels;
        }
    }
}