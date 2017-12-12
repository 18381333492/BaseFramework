using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{
    /// <summary>
    /// 发送微信公众号模板消息所需参数的Model
    /// </summary>
    public class SendTemplateInfoModel: WeChatModel
    {
        /// <summary>
        /// 接收者openid
        /// </summary>
        public string sOpenId { get; set; }

        /// <summary>
        /// 模板Id
        /// </summary>
        public string sTemplateId { get; set; }


        /// <summary>
        /// 模板跳转的链接
        /// </summary>
        public string sUrl { get; set; }


        /// <summary>
        /// 模板发送的消息内容
        /// </summary>
        public string sBody { get; set; }
    }
}
