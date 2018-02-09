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
        /// 执行事务
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int ExcuteTransaction(string sqlCommand, object parameter);

        /// <summary>
        /// 执行存储过程返回查询结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        int ExcuteProcedure(string sProcedureName, SqlServerDbParameters Parameters);
    }
}
