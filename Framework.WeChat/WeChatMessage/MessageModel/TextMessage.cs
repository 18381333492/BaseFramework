using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 微信发送文本消息的实体
    /// </summary>
    public class TextMessage:IRMessage
    {
        /// <summary>
        /// 文本内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }
    }
}
