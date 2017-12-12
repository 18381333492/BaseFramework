using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Framework.Utility.Tools
{
    public class ConfigHelper
    {

        /// <summary>
        /// 根据sKey获取AppSetting的值
        /// </summary>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string GetAppSetting(string sKey)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(sKey))
                result= ConfigurationManager.AppSettings[sKey];
            return result;
        }

        /// <summary>
        /// 获取数据库连接串
        /// </summary>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string GetConnString(string sKey)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(sKey))
            {
                ConnectionStringSettings cConstrset = ConfigurationManager.ConnectionStrings[sKey];
                result = cConstrset.ConnectionString;
            }
            return result;
        }

    }
}
