using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Tools
{
    public class GuidHelper
    {

        /// <summary>
        /// 返回唯一的GUID
        /// </summary>
        /// <returns></returns>
        public static Guid NewGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// 获取字符串的GUID
        /// </summary>
        /// <returns></returns>
        public static string NewGuidString()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
