using Dapper;
using Framework.Utility.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Utility;

namespace Framework.DBAccess.Dapper
{
    public class DbQueryManagement : DbBases
    {
        /// <summary>
        /// 判断泛型是否是字典类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual bool DoIsDictionary<T>()
        {
            return typeof(T).GetInterface("IDictionary")==null? false:true;      
        }

        /// <summary>
        /// 根据条件查询是否存在相应的数据
        /// </summary>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sqlCommand">sql命令</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        protected virtual bool DoAny(IDbConnection conn, string sqlCommand, object parameter)
        {
            var ret = conn.Query(sqlCommand, parameter, null, true, null, CommandType.Text);
            return ret.Count() > 0 ? true : false;
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sqlCommand">Sql语句</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        protected T DoSingleQuery<T>(IDbConnection conn, string sqlCommand, Object parameter = null) where T : new()
        {
            if (DoIsDictionary<T>())
            {//泛型是字典类型
                var ret = conn.Query(sqlCommand, parameter, null, true, null, CommandType.Text)
                       .Select(m => ((IDictionary<string, object>)m)
                       .ToDictionary(k => k.Key, v => v.Value))
                       .FirstOrDefault<IDictionary<string, object>>();
                //类型转化
                return (T)ret;
            }
            else
            {
                var ret = conn.QueryFirstOrDefault<T>(sqlCommand, parameter, null, null, CommandType.Text);
                return ret;
            }
        }

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sqlCommand">Sql语句</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        protected virtual IList<T> DoQueryList<T>(IDbConnection conn, string sqlCommand, Object parameter = null)
        {
            if (DoIsDictionary<T>())
            {//泛型是字典类型
                var ret = conn.Query(sqlCommand, parameter, null, true, null, CommandType.Text)
                       .Select(m => ((IDictionary<string, object>)m)
                       .ToDictionary(k => k.Key, v => v.Value))
                       .ToList<IDictionary<string,object>>();
                return ret.Select(m=>(T)m).ToList();
            }
            else
            {
                var ret = conn.Query<T>(sqlCommand, parameter, null, true, null, CommandType.Text).ToList();
                return ret;
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
        protected virtual PageResult DoPaginationQuery(IDbConnection conn, string sqlCommand, PageInfo pageInfo, object parameter = null)
        {
            //声明动态参数
            //DynamicParameters Parameters = new DynamicParameters();
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
            var ret = conn.Query(sSql, parameter, null, true, null, CommandType.Text).ToList();
            if (ret.Count > 0)
            {
                //随意抽一条做最大条数记录
                result.total =Convert.ToInt32(ret[0].MaxRows);
                result.rows = ret;
            }
            else
            {
                result.rows =new List<object>();
            }
            return result;
        }

        ///// <summary>
        ///// 执行存储过程返回结果集
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sProcedureName"></param>
        ///// <param name="Parameters"></param>
        ///// <returns></returns>
        //protected IList<T> DoQueryProcedure<T>(IDbConnection conn, string sProcedureName, DynamicParameters Parameters)
        //{
        //    if (DoIsDictionary<T>())
        //    {//泛型是字典类型
        //        var ret = conn.Query(sProcedureName, Parameters, null, true, null, CommandType.StoredProcedure)
        //                  .Select(m => ((IDictionary<string, object>)m)
        //                  .ToDictionary(k => k.Key, v => v.Value))
        //                  .ToList<IDictionary<string, object>>();
        //        return ret.Select(m => (T)m).ToList();
        //    }
        //    else
        //    {
        //        var ret = conn.Query<T>(sProcedureName, Parameters, null, true, null, CommandType.StoredProcedure).ToList();
        //        return ret;
        //    }
        //}

    }
}
