using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace HoldingTaxWebApp.Gateway
{
    public class DefaultGateway
    {
        private readonly string _conString = WebConfigurationManager.ConnectionStrings["ConnStrHTAS"].ConnectionString;

        public SqlConnection Sql_Connection { get; set; }
        public SqlCommand Sql_Command { get; set; }
        public SqlDataReader Data_Reader { get; set; }
        public SqlDataAdapter Data_Adapter { get; set; }
        public DataTable Data_Table { get; set; }
        public DataSet Data_Set { get; set; }
        public string Sql_Query { get; set; }
        public StringBuilder ErrorMessages { get; set; }

        public DefaultGateway()
        {
            Sql_Connection = new SqlConnection(_conString);
        }


    }
}