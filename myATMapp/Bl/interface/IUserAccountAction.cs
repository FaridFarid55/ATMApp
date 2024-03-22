using myATMapp.Domain.Models;
using myATMapp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myATMapp.Bl.Class;

namespace myATMapp.Bl.@interface
{
    public interface IUserAccountAction
    {
        // properties
        public List<TransActionModel> ListTransAction { get; set; }
    }
}
