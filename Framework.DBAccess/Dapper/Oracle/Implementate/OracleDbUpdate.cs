﻿using Dapper;
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

    public class OracleDbUpdate : DbUpdateManager
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
    }
}
