using Framework.Utility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DBAccess.Dapper
{
    /// <summary>
    /// Oracle数据库DB查询接口
    /// </summary>
    public interface IOracleDbQuery : IDbQuery
    {

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="pageInfo"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageResult PaginationQuery<T>(string sqlCommand, PageInfo pageInfo, OracleDbParameters Parameters);
    }
}
