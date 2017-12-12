using Framework.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceLogs;

namespace Framework.DBAccess.EF
{
    public static class ContextManager
    {

        /// <summary>
        /// 日志记录
        /// </summary>
        private static ILogger logger = LoggerManager.Instance.GetSLogger("EF");


        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        public static void Insert<T>(this Entities Context, T entry) where T : class, new()
        {
            Context.Set<T>().Add(entry);
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        public static void Update<T>(this Entities Context,T entry) where T : class, new()
        {
            Context.Set<T>().Attach(entry);
            Context.Entry<T>(entry).State = EntityState.Modified;//编辑
        }


        /// <summary>
        /// 物理删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        public static void Delete<T>(this Entities Context,T entry) where T : class, new()
        {
            Context.Set<T>().Attach(entry);
            Context.Entry<T>(entry).State = EntityState.Deleted;//删除
        }


        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Ids">主键Ids集合</param>
        public static int Cancel<T>(this Entities Context,string Ids) where T : class, new()
        {
            try
            {
                string Args = string.Empty;
                SqlParameter[] Parameter= GetSqlParameter(Ids,out Args);
                int res = Context.Database.
                     ExecuteSqlCommand(string.Format(@"UPDATE [{0}] SET bIsDeleted = 1 WHERE ID IN({1})", typeof(T).Name, Args),Parameter);
               return res;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return -1;
            }
           
        }

        /// <summary>
        /// 物理删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Ids">主键Ids集合,以逗号隔开</param>
        public static int Delete<T>(this Entities Context,string Ids) where T : class, new()
        {
            try
            {
                string Args = string.Empty;
                SqlParameter[] Parameter = GetSqlParameter(Ids, out Args);
                int res = Context.Database.
                    ExecuteSqlCommand(string.Format(@"DELETE [{0}] WHERE ID IN({1})", typeof(T).Name, Args), Parameter);
                return res;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return -1;
            }
        }


        /// <summary>
        /// 防止SQL注入,组织参数
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public static SqlParameter[] GetSqlParameter(string Ids,out string Args)
        {
            var IdArray = Ids.Split(',').ToList();
            List<SqlParameter> SqlPars = new List<SqlParameter>();
            var parameter = new List<string>();
            for (var i = 0; i < IdArray.Count; i++)
            {
                string m = string.Format("@Parameter{0}", i + 1);
                parameter.Add(m);
                SqlPars.Add(new SqlParameter(m, IdArray[i]));
            }
            Args = string.Join(",", parameter);
            return SqlPars.ToArray();
        }


        /// <summary>
        /// 根据Sql语句执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">Sql语句</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExcuteBySql(this Entities Context,string sql, params object[] param)
        {
            try
            {
                int res = Context.Database.ExecuteSqlCommand(sql, param);
                return res;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return -1;
            }
        }


        /// <summary>
        /// 无操作日志提交操作
        /// </summary>
        /// <returns></returns>
        public static int SaveExtendChanges(this Entities Context)
        {
            try
            {
                int res=Context.SaveChanges();
                return res;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return -1;
            }
        }
    }
}
