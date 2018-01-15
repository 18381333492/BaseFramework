using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 响应给微信系统的实体基类
    /// </summary>
    public class ISMessage
    {
        /// <summary>
        ///接收方帐号（收到的OpenID）
        /// </summary>
        public string ToUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 开发者微信号(公众号的原始ID)
        /// </summary>
        public string FromUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 发送时间(时间戳)
        /// </summary>
        public int CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 响应的消息类型
        /// </summary>
        public string MsgType
        {
            get;
            set;
        }
    }
}
