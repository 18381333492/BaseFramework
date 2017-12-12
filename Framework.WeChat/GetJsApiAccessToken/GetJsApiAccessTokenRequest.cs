using Framework.Utility.Tools;
using Framework.WeChat.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{
    public class GetJsApiAccessTokenRequest:WeChatRequest,IWeChatRequest
    {
        public GetJsApiAccessTokenRequest()
        {
            sRequestUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";
        }

        /// <summary>
        /// 执行获取调用JS接口的凭证的请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Execute<T>(WeChatModel Model)
        {
            GetJsApiAccessTokenModel model = Model as GetJsApiAccessTokenModel;
            sRequestUrl = string.Format(sRequestUrl, model.sAccessToken);
            string result = HttpHelper.HttpGet(sRequestUrl);
            T Respone = JsonHelper.Deserialize<T>(result);
            return Respone;
        }
    }
}
