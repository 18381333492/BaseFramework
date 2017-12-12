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
    /// 微信公众号创建自定义菜单请求
    /// </summary>
    public class CreateAutoMenuRequest: WeChatRequest,IWeChatRequest
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public CreateAutoMenuRequest()
        {
            sRequestUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
        }


        /// <summary>
        /// 执行创建自定义菜单的请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public T Execute<T>(WeChatModel Model)
        {
            CreateAutoMenuModel model = Model as CreateAutoMenuModel;
            sRequestUrl = string.Format(sRequestUrl, model.sAccessToken);
            string result = HttpHelper.HttpPost(sRequestUrl,model.sBody);
            T Respone = JsonHelper.Deserialize<T>(result);
            return Respone;
        }
    }
}
