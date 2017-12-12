using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{
    public class GetAccessTokenRespone: WeChatRespone
    {
        /// <summary>
        /// 调用凭证
        /// </summary>
        public string access_token { get; set; }


        /// <summary>
        /// 有效时间(单位秒)
        /// </summary>
        public int expires_in { get; set; }
    }
}
