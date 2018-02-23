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

    public class SqlServerDbQuery : DbQueryManager
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

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="pageInfo"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected PageResult DoPaginationQuery(IDbConnection conn, string sqlCommand, PageInfo pageInfo, DynamicParameters Parameters)
        {
            string sSql = string.Format(@"DECLARE @rows int 
                                                SELECT @rows=COUNT(*) FROM({0}) as entry 
                                                SELECT  TOP {1} *,@rows MaxRows FROM
                                                (SELECT  ROW_NUMBER() OVER(ORDER BY {2} {3}) AS Number,*
                                                FROM ({0}) AS query) AS entry 
                                                WHERE  Number>{1}*({4}-1) ", sqlCommand,
                                          pageInfo.rows,
                                          pageInfo.sort,
                                          pageInfo.order.ToString(),
                                          pageInfo.page);
            var result = new PageResult();
            result.page = pageInfo.page;//当前页码数
            //获取查询结果   
            var ret = conn.Query(sSql, Parameters, null, true, null, CommandType.Text).ToList();
            if (ret.Count > 0)
            {
                //随意抽一条做最大条数记录
                result.total = Convert.ToInt32(ret[0].MaxRows);
                result.rows = ret;
            }
            else
            {
                result.rows = new List<object>();
            }
            return result;
        }

        /// <summary>
        /// 执行存储过程返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        protected IList<T> DoQueryProcedure<T>(IDbConnection conn, string sProcedureName, DynamicParameters Parameters)
        {
            if (DoIsDictionary<T>())
            {//泛型是字典类型
                var ret = conn.Query(sProcedureName, Parameters, null, true, null, CommandType.StoredProcedure)
                          .Select(m => ((IDictionary<string, object>)m)
                          .ToDictionary(k => k.Key, v => v.Value))
                          .ToList<IDictionary<string, object>>();
                return ret.Select(m => (T)m).ToList();
            }
            else
            {
                var ret = conn.Query<T>(sProcedureName, Parameters, null, true, null, CommandType.StoredProcedure).ToList();
                return ret;
            }
        }
    }
}
