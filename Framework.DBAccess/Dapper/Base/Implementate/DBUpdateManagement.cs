using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Framework.DBAccess.Dapper
{
    public class DbUpdateManagement : DbBase
    {

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected int DoExcuteTransaction(IDbConnection conn,string sqlCommand, object parameter)
        {
            //开始事务
            IDbTransaction transaction=conn.BeginTransaction();
            try
            {
                int res = conn.Execute(sqlCommand, parameter, transaction, null, CommandType.Text);
                transaction.Commit();
                return res;
            }
            catch (Exception ex)
            {
                //回滚
                transaction.Rollback();
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return -1;
            }
        }

        /// <summary>
        /// 执行储存过程
        /// </summary>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sProcedureName">存储过程名称</param>
        /// <param name="Parameters">参数</param>
        protected int DoExcuteProcedure(IDbConnection conn,string sProcedureName, DynamicParameters Parameters)
        {
            int res=conn.Execute(sProcedureName, Parameters, null, null, CommandType.StoredProcedure);
            return res;
        }
    }
}
