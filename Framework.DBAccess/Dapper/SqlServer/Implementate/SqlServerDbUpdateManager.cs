using Dapper;
using Framework.Utility.Models;
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
    /// SqlServer数据库更新操作
    /// </summary>
    public class SqlServerDbUpdateManager : SqlServerDbUpdate, ISqlServerDbUpdate
    {

        /// <summary>
        /// 执行SQl语句
        /// </summary>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        public int ExcuteSql(string sProcedureName, object Parameters)
        {
            IDbConnection conn = null;
            try
            {
                conn = GetSqlConnection();
                if (conn == null) throw new ApplicationException("未获取到连接对象.");
                return DoExcuteSql(conn, sProcedureName, Parameters);
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
        /// 执行SQl语句
        /// </summary>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        public int ExcuteSql(string sProcedureName, DbParameters Parameters)
        {
            IDbConnection conn = null;
            try
            {
                conn = GetSqlConnection();
                if (conn == null) throw new ApplicationException("未获取到连接对象.");
                return DoExcuteSql(conn, sProcedureName, Parameters.GetParameters());
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
    }
}