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
    public class DbUpdateManager : DbUpdateManagement, IDbUpdate
    {

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int ExcuteTransaction(string sqlCommand, object parameter)
        {
            IDbConnection conn = null;
            try
            {
                conn = GetSqlConnection();
                if (conn == null) throw new ApplicationException("未获取到连接对象.");
                return DoExcuteTransaction(conn, sqlCommand, parameter);
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

        /// <summary>
        /// 执行储存过程
        /// </summary>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        public int ExcuteProcedure(string sProcedureName, SqlDbParameters Parameters)
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
    }
}
