using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{
    /// <summary>
    /// 获取调用JS接口的凭证的请求的响应
    /// </summary>
    public class GetJsApiAccessTokenRespone:WeChatRespone
    {

        /// <summary>
        /// js接口调用凭证
        /// </summary>
        public string ticket { get; set; }

        /// <summary>
        /// 凭证有效期(单位秒)
        /// </summary>
        public int expires_in { get; set; }

    }
}
