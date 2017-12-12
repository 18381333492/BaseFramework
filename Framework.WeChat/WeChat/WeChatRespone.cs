using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{
    public class WeChatRespone
    {
        /// <summary>
        /// 返回的错误码
        /// </summary>
        public int errcode { get; set; } = 0;

        /// <summary>
        /// 返回的错误消息
        /// </summary>
        public string errmsg { get; set; }
    }
}
