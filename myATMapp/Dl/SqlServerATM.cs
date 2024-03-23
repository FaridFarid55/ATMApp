using myATMapp.Domain.Models;
using myATMapp.Sirelze;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myATMapp.Dal
{
    public class SqlServerATM : IReadeATM
    {
        private string sFile = string.Empty;
        private ISirelzeATm _serlizer;
        // Constrctor
        public SqlServerATM(ISirelzeATm serlizer)
        {
            _serlizer = serlizer;
        }

        public string ReadeATM()
        {

            string ConnectionString = "server=FARID\\MSSQLSERVER01;database=ATM;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                // open the connection  
                conn.Open();

                //Create a SqlDataAdapter object  
                using (SqlDataAdapter adapter = new SqlDataAdapter("select * from dbATM", conn))
                {
                    // Call DataAdapter's Fill method to fill data from the  
                    // Data Adapter to the DataSet  
                    DataSet ds = new DataSet("Customers");
                    adapter.Fill(ds);
                    //
                    sFile = _serlizer.Sirelze(ds.Tables[0]);

                    // return
                    return sFile;

                }
            }
        }
    }
}
