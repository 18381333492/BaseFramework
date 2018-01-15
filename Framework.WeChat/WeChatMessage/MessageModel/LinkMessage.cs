using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 微信发送链接消息的实体
    /// </summary>
    public class LinkMessage:IRMessage
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url
        {
            get;
            set;
        }
    }
}
