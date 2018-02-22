using Dapper;
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
    public class DbUpdateManager : DbBase
    {

        /// <summary>
        /// 执行储存过程
        /// </summary>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        public int ExcuteProcedure(string sProcedureName, DbParameters Parameters)
        {
            IDbConnection conn = null;
            try
            {
                conn = GetSqlConnection();
                if (conn == null) throw new ApplicationException("未获取到连接对象.");
                return DoExcuteProcedure(conn, sProcedureName, Parameters.GetParameters());
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return -1;
            }
            finally
            {
                CloseConnect(conn);
            }
        }


        #region DbUpdate的Dapper实现

        /// <summary>
        /// 执行储存过程
        /// </summary>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sProcedureName">存储过程名称</param>
        /// <param name="Parameters">参数</param>
        protected int DoExcuteProcedure(IDbConnection conn, string sProcedureName, DynamicParameters Parameters)
        {
            int res = conn.Execute(sProcedureName, Parameters, null, null, CommandType.StoredProcedure);
            return res;
        }

        #endregion
    }
}
