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
    /// 数据库DB的参数
    /// </summary>
    public class DbParameters
    {
        private DynamicParameters Parameters;

        public DbParameters()
        {
            Parameters = new DynamicParameters();
        }

        /// <summary>
        /// 获取DB参数
        /// </summary>
        /// <returns></returns>
        public DynamicParameters GetParameters()
        {
            return Parameters;
        }


        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        public virtual void Add(string name, object value, DbType? dbType = null, ParameterDirection? direction = null, int? size = null)
        {
            Parameters.Add(name, value, dbType, direction, size);
        }

        /// <summary>
        /// 获取输出参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual object Get(string name)
        {
            var ret = Parameters.Get<object>(name);
            return ret;
        }

        /// <summary>
        /// 获取输出参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual T Get<T>(string name)
        {
            var ret = Parameters.Get<T>(name);
            return ret;
        }
    }
}
