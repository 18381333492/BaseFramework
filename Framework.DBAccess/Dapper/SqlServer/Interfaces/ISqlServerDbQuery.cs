using Framework.Utility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DBAccess.Dapper
{
    /// <summary>
    /// SqlServer数据库DB查询接口
    /// </summary>
    public interface ISqlServerDbQuery:IDbQuery
    {

        /// <summary>
        /// 执行存储过程返回查询结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        IList<T> QueryProcedure<T>(string sProcedureName, SqlServerDbParameters Parameters);
    }
}
