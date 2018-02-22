using Dapper;
using Framework.Utility.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Utility.Tools;

namespace Framework.DBAccess.Dapper
{

    public class OracleDbBase : DbQueryManager
    {
        /// <summary>
        /// 获取Oracle的数据库链接
        /// </summary>
        /// <returns></returns>
        protected override IDbConnection GetSqlConnection()
        {
            try
            {
                OracleConnection conn = new OracleConnection(sConnectionString);
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
        /// 获取SQl(oracle的别名的处理)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected string GetSql(string sqlCommand)
        {
            sqlCommand = sqlCommand.Replace("[", "\"");
            sqlCommand = sqlCommand.Replace("]", "\"");
            return sqlCommand;
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
            //统计的SQl
            string CountSql = string.Format("SELECT COUNT(*) FROM ({0})", sqlCommand);
            var Total = conn.Query<int>(CountSql.ToString(), Parameters, null, true, null, CommandType.Text);

            //声明动态参数
            Parameters.Add("PageIndex", pageInfo.page, DbType.Int32);
            Parameters.Add("PageSize", pageInfo.rows, DbType.Int32);
            //结果集的SQl
            string sSql = string.Format(@"SELECT * FROM 
                                       (SELECT ROWNUM LINENUM,t.* FROM ({0}) t WHERE ROWNUM<=(:PageIndex*:PageSize)) 
                                        WHERE LINENUM>(:PageIndex-1)*:PageSize", sqlCommand);
            var result = new PageResult();
            var ret = conn.Query(sSql, Parameters, null, true, null, CommandType.Text).ToList();
            result.rows = ret;

            result.page = pageInfo.page;
            result.total = Total.FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="pageInfo"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected PageResult DoPaginationQuery<T>(IDbConnection conn, string sqlCommand, PageInfo pageInfo, DynamicParameters Parameters)
        {
            //统计的SQl
            string CountSql = string.Format("SELECT COUNT(*) FROM ({0})", sqlCommand);
            var Total = conn.Query<int>(CountSql.ToString(), Parameters, null, true, null, CommandType.Text);

            //声明动态参数
            Parameters.Add("PageIndex", pageInfo.page, DbType.Int32);
            Parameters.Add("PageSize", pageInfo.rows, DbType.Int32);
            //结果集的SQl
            string sSql = string.Format(@"SELECT * FROM 
                                       (SELECT ROWNUM LINENUM,t.* FROM ({0}) t WHERE ROWNUM<=(:PageIndex*:PageSize)) 
                                        WHERE LINENUM>(:PageIndex-1)*:PageSize", sqlCommand);
            var result = new PageResult();
            if (DoIsDictionary<T>())
            {
                var ret = conn.Query(sSql, Parameters, null, true, null, CommandType.Text)
                         .Select(m => ((IDictionary<string, object>)m)
                         .ToDictionary(k => k.Key, v => v.Value))
                         .ToList<Dictionary<string, object>>();
                //转化成相应的类型
                List<T> res = JsonHelper.Deserialize<List<T>>(JsonHelper.ToJsonString(ret));
                result.rows = res;
            }
            else
            {
                var ret = conn.Query<T>(sSql, Parameters, null, true, null, CommandType.Text).ToList();
                result.rows = ret;
            }
            result.page = pageInfo.page;
            result.total = Total.FirstOrDefault();
            return result;
        }

    }
}
