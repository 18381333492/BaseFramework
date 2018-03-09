using Dapper;
using Framework.Utility.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DBAccess.Dapper
{

    public class MySqlDbUpdate : DbUpdateManager
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
