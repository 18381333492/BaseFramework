using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 点击菜单拉取消息时的事件推送实体
    /// </summary>
    public class ClickEvent:IRMessage
    {
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey
        {
            get;
            set;
        }
    }
}
