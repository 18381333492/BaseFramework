using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Models
{

    /// <summary>
    /// 后台登录缓存的信息
    /// </summary>
    [Serializable]
    public class LoginCacheInfo
    {
        /// <summary>
        /// 后台主键ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string sName { get; set; } 

        /// <summary>
        /// 头像
        /// </summary>
        public string sHeadImg { get; set; }
    }
}
