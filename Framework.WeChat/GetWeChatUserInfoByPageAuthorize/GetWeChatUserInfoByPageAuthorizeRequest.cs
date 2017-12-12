using Framework.Utility.Tools;
using Framework.WeChat.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{
    /// <summary>
    /// 通过网页授权获取微信用户信息请求
    /// </summary>
    public class GetWeChatUserInfoByPageAuthorizeRequest : WeChatRequest, IWeChatRequest
    {

        public GetWeChatUserInfoByPageAuthorizeRequest()
        {
            sRequestUrl = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";
        }


        /// <summary>
        /// 执行通过网页授权获取微信用户信息的请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Execute<T>(WeChatModel Model)
        {
            GetWeChatUserInfoByPageAuthorizeModel model = Model as GetWeChatUserInfoByPageAuthorizeModel;
            //第一步 获取openid和access_token
            IWeChatRequest request = new GetOpenIdByPageAuthorizeRequest();
            GetOpenIdByPageAuthorizeModel obj = new GetOpenIdByPageAuthorizeModel() {sAppId= model.sAppId,sAppSecret=model.sAppSecret,sCode=model.sCode };
            GetOpenIdByPageAuthorizeRespone res=request.Execute<GetOpenIdByPageAuthorizeRespone>(obj);
            //第二步 拉取用户信息
            sRequestUrl = string.Format(sRequestUrl, res.access_token, res.openid);
            string result = HttpHelper.HttpGet(sRequestUrl);
            T respone = JsonHelper.Deserialize<T>(result);
            return respone;
        }
    }
}
