using DapperLambda;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Tdf.Act.DapperLambda
{
    public class DBHelper
    {
        private static readonly string SqlServerConnString =
        ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ToString();

        public static DbContext GetContext()
        {
            return new DbContext().ConnectionString(SqlServerConnString);
        }

        public static IDbConnection GetDapperConnection()
        {
            return new SqlConnection(SqlServerConnString);
        }


    }
}
