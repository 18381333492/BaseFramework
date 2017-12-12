using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{
    /// <summary>
    /// 通过网页授权获取微信用户信息所需的参数Model
    /// </summary>
    public class GetWeChatUserInfoByPageAuthorizeModel:WeChatModel
    {
        /// <summary>
        ///  code作为换取access_token的票据
        ///  每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。
        /// </summary>
        public string sCode { get; set; }
    }
}
