using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.WeChat.Tool;
using Framework.Utility.Tools;

namespace Framework.WeChat
{
    /// <summary>
    /// 根据微信用户的OpenId获取用户信息
    /// 注意:没有关注的用户获取不到用户信息
    /// </summary>
    public class GetWeChatUserInfoRequest : WeChatRequest, IWeChatRequest
    {

        public GetWeChatUserInfoRequest()
        {
            sRequestUrl = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";
        }


        /// <summary>
        /// 执行获取微信用户信息的请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Execute<T>(WeChatModel Model)
        {
            GetWeChatUserInfoModel model = Model as GetWeChatUserInfoModel;
            sRequestUrl = string.Format(sRequestUrl, model.sAccessToken, model.sOpenId);
            string result=HttpHelper.HttpGet(sRequestUrl);
            T respone = JsonHelper.Deserialize<T>(result);
            return respone;
        }
    }
}
