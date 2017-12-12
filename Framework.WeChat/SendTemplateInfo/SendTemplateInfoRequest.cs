using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Utility.Tools;
using Framework.WeChat.Tool;

namespace Framework.WeChat
{

    /// <summary>
    /// 发送模板消息的请求
    /// </summary>
    public class SendTemplateInfoRequest : WeChatRequest, IWeChatRequest
    {
       
        public SendTemplateInfoRequest()
        {
            sRequestUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";
        }


        /// <summary>
        /// 执行发送模板消息的请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Execute<T>(WeChatModel Model)
        {
            SendTemplateInfoModel model = Model as SendTemplateInfoModel;
            sRequestUrl = string.Format(sRequestUrl,model.sAccessToken);
            var Data = new
            {
                touser = model.sOpenId,
                template_id = model.sTemplateId,
                url = model.sUrl,
                data = model.sBody
            };
            string DataString = JsonHelper.ToJsonString(Data);
            string result = HttpHelper.HttpPost(sRequestUrl, DataString);
            T Respone = JsonHelper.Deserialize<T>(result);
            return Respone;
        }
    }
}
