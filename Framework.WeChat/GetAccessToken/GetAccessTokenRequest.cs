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
    /// 获取微信接口的调用凭证
    /// </summary>
    public class GetAccessTokenRequest : WeChatRequest, IWeChatRequest
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public GetAccessTokenRequest()
        {
            sRequestUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        }

       
        /// <summary>
        /// 执行获取微信接口调用凭证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public T Execute<T>(WeChatModel Model)
        {
            sRequestUrl = string.Format(sRequestUrl, Model.sAppId, Model.sAppSecret);
            string result=HttpHelper.HttpGet(sRequestUrl);
            T Respone = JsonHelper.Deserialize<T>(result);
            return Respone;
        }
    }
}
