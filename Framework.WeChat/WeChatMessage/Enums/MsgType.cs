using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 微信的消息类型
    /// </summary>
    public enum MsgType
    {

        TEXT,             //文本

        IMAGE,           //图片

        VOICE,           //声音

        VIDEO,           //视频

        SHORTVIDEO,      //小视屏

        LOCATION,        //位置

        LINK,            //链接

        EVENT            //事件
    }
}
