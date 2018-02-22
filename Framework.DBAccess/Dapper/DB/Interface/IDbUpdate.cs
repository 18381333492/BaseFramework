using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DBAccess.Dapper
{
    /// <summary>
    /// DB查询更新接口
    /// </summary>
    public interface IDbUpdate
    {
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        int ExcuteSql(string Sql, object Parameters = null);

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        int ExcuteSql(string Sql, DbParameters Parameters = null);

        /// <summary>
        /// 执行存储过程返回查询结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        int ExcuteProcedure(string sProcedureName, DbParameters Parameters);
    }
}
