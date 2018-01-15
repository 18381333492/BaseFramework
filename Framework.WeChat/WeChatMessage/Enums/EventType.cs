using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 微信的事件类型
    /// </summary>
    public enum EventType
    {
        SUBSCRIBE,      //订阅/关注事件

        UNSUBSCRIBE,    //取消订阅/取消关注事件

        SCAN,           //带参数二维码事件
                
        LOCATION,       //上报地理位置事件

        CLICK,          //点击菜单拉取消息时的事件推送

        VIEW           //点击菜单跳转链接时的事件推送
    }
}
