using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 点击菜单跳转链接时的事件推送实体
    /// </summary>
    public class ViewEvent:IRMessage
    {
        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey
        {
            get;
            set;
        }
    }
}
