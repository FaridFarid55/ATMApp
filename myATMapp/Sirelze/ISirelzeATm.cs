
using myATMapp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Sirelze
{
    public interface ISirelzeATm
    {
        public List<UserAccountActionModel> Desirelze(string json);
    }
}
