using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 微信推送给系统消息的实体基类
    /// </summary>
    public class IRMessage
    {
        /// <summary>
        /// 开发者微信号(公众号的原始ID)
        /// </summary>
        public string ToUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 发送方帐号(微信用户的OpenID)
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
        /// 发送的消息类型
        /// </summary>
        public string MsgType
        {
            get;
            set;
        }

        /// <summary>
        /// 发送事件类型
        /// </summary>
        public string Event
        {
            get;
            set;
        }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId
        {
            get;
            set;
        }
    }
}
