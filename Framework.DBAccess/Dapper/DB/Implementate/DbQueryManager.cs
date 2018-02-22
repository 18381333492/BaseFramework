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
    /// 数据库Db的查询
    /// </summary>
    public class DbQueryManager:DbBase
    {

        /// <summary>
        /// 判断泛型是否是字典类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual bool DoIsDictionary<T>()
        {
            return typeof(T).GetInterface("IDictionary") == null ? false : true;
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
        protected virtual T DoSingleQuery<T>(IDbConnection conn, string sqlCommand, object parameter = null) where T : new()
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
        protected virtual IList<T> DoQueryList<T>(IDbConnection conn, string sqlCommand, object parameter = null)
        {
            if (DoIsDictionary<T>())
            {//泛型是字典类型
                var ret = conn.Query(sqlCommand, parameter, null, true, null, CommandType.Text)
                       .Select(m => ((IDictionary<string, object>)m)
                       .ToDictionary(k => k.Key, v => v.Value))
                       .ToList<IDictionary<string, object>>();
                return ret.Select(m => (T)m).ToList();
            }
            else
            {
                var ret = conn.Query<T>(sqlCommand, parameter, null, true, null, CommandType.Text).ToList();
                return ret;
            }
        }
    }
}