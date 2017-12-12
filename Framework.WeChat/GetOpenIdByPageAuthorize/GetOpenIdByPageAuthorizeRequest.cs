using Framework.WeChat.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Utility.Tools;

namespace Framework.WeChat
{
    /// <summary>
    /// 根据网页授权获取微信用户的OpendId请求
    /// </summary>
    public class GetOpenIdByPageAuthorizeRequest:WeChatRequest,IWeChatRequest
    {
        public GetOpenIdByPageAuthorizeRequest()
        {
            sRequestUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        }

        /// <summary>
        /// 执行获取微信用户OpenId的请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Execute<T>(WeChatModel Model)
        {
            GetOpenIdByPageAuthorizeModel model = Model as GetOpenIdByPageAuthorizeModel;
            sRequestUrl = string.Format(sRequestUrl, model.sAppId, model.sAppSecret, model.sCode);
            string result = HttpHelper.HttpGet(sRequestUrl);
            T respone = JsonHelper.Deserialize<T>(result);
            return respone; 
        }
    }
}
