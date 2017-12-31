using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DBAccess.Dapper
{
    /// <summary>
    /// Sql Server数据库DB参数的封装
    /// </summary>
    public class SqlDbParameters
    {
        private DynamicParameters SqlParameters;
        public SqlDbParameters()
        {
            SqlParameters = new DynamicParameters();
        }
     
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <returns></returns>
        public DynamicParameters GetParameters()
        {
            return SqlParameters;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        public void Add(string name, object value, DbType? dbType=null, ParameterDirection? direction=null, int? size=null)
        {
            SqlParameters.Add(name, value, dbType, direction, size);
        }

        /// <summary>
        /// 获取输出参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public object Get(string name)
        {
            var ret = SqlParameters.Get<object>(name);
            return ret;
        }

        /// <summary>
        /// 获取输出参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Get<T>(string name)
        {
            var ret = SqlParameters.Get<T>(name);
            return ret;
        }
    }
}
