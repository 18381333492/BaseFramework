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
    public class DbUpdateManager:DbBase
    {

        /// <summary>
        /// 执行储存过程
        /// </summary>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sqlCommand">sql语句</param>
        /// <param name="Parameters">参数</param>
        protected int DoExcuteSql(IDbConnection conn, string sqlCommand, object Parameters)
        {
            int res = conn.Execute(sqlCommand, Parameters, null, null, CommandType.Text);
            return res;
        }

        /// <summary>
        /// 执行储存过程
        /// </summary>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sqlCommand">sql语句</param>
        /// <param name="Parameters">参数</param>
        protected int DoExcuteSql(IDbConnection conn, string sqlCommand, DynamicParameters Parameters)
        {
            int res = conn.Execute(sqlCommand, Parameters, null, null, CommandType.Text);
            return res;
        }

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
    }
}
