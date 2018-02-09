using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceLogs;

namespace Framework.DBAccess.Dapper
{
    /// <summary>
    /// 数据库的链接
    /// </summary>
    public class SqlServerDbBase : DbBases
    {
        /// <summary>
        /// 获取SqlServer的数据库链接
        /// </summary>
        /// <returns></returns>
        protected override IDbConnection GetSqlConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(sConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return null;
            }
        }
    }
}
