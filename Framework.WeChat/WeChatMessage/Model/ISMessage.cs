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
        /// 无参构造函数（xml序列化必须提供无参构造函数）
        /// </summary>
        public ISMessage() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="request"></param>
        public ISMessage(IRMessage model)
        {
            this.ToUserName = model.FromUserName;
            this.FromUserName = model.ToUserName;
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.CreateTime = Convert.ToInt32(ts.TotalSeconds);
        }

        /// <summary>
        ///接收方帐号（收到的OpenID）
        /// </summary>
        [CDATA]
        public string ToUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 开发者微信号(公众号的原始ID)
        /// </summary>
        [CDATA]
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
    }
}
