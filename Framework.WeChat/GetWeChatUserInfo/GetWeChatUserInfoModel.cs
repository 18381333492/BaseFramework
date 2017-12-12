using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{

    /// <summary>
    /// 获取微信用户信息所需要的参数Model
    /// </summary>
    public class GetWeChatUserInfoModel:WeChatModel
    {
        /// <summary>
        /// 微信用户OpenId
        /// </summary>
        public string sOpenId { get; set; }
    }
}
