using Framework.WeChat.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 对微信处理消息的封装
    /// </summary>
    public class ActioningRequest
    {
        /// <summary>
        /// 消息接口基类
        /// </summary>
        private IAction Action { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Action"></param>
        public ActioningRequest(IAction Action)
        {
            this.Action = Action;
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xmlcontent"></param>
        /// <returns></returns>
        public string ProcessMessage(MsgType type, string xmlcontent)
        {
            if (null == Action) return string.Empty;
            string sResult = string.Empty;
            switch (type)
            {
                case MsgType.TEXT:         //文本
                    sResult = Action.HandleText(XmlHelper.Deserialize<TextMessage>(xmlcontent));
                    break;
                case MsgType.IMAGE:        //图片
                    sResult = Action.HandleImage(XmlHelper.Deserialize<ImageMessage>(xmlcontent));
                    break;
                case MsgType.LINK:         //链接
                    sResult = Action.HandleLink(XmlHelper.Deserialize<LinkMessage>(xmlcontent));
                    break;
                case MsgType.VOICE:        //语音
                    sResult = Action.HandleVoice(XmlHelper.Deserialize<VoiceMessage>(xmlcontent));
                    break;
                case MsgType.SHORTVIDEO:   //小视频
                    sResult = Action.HandleVideo(XmlHelper.Deserialize<VideoMessage>(xmlcontent));
                    break;
                case MsgType.VIDEO:        //视频
                    sResult = Action.HandleVideo(XmlHelper.Deserialize<VideoMessage>(xmlcontent));
                    break;
                case MsgType.LOCATION:     //位置
                    sResult = Action.HandleLocation(XmlHelper.Deserialize<LocationMessage>(xmlcontent));
                    break;
            }
            return sResult;
        }

        /// <summary>
        /// 处理微信推送的事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xmlcontent"></param>
        /// <returns></returns>
        public string ProcessEvent(EventType type, string xmlcontent)
        {
            if (null == Action) return string.Empty;
            string sResult = string.Empty;
            switch (type)
            {
                case EventType.SUBSCRIBE:   //关注
                    sResult = Action.HandleSubscribeEvent(XmlHelper.Deserialize<SubscribeEvent>(xmlcontent));
                    break;
                case EventType.UNSUBSCRIBE: //取消关注                   
                    sResult = Action.HandleUnSubscribeEvent(XmlHelper.Deserialize<UnSubscribeEvent>(xmlcontent));
                    break;
                case EventType.SCAN:        //关注后的扫码推送                
                    sResult = string.Empty;
                    break;
                case EventType.LOCATION:    //上报地理位置推送              
                    sResult = string.Empty;
                    break;
                case EventType.CLICK:       //点击菜单按钮        
                    sResult = string.Empty;
                    break;
                case EventType.VIEW:        //点击菜单按钮跳转url           
                    sResult = string.Empty;
                    break;
            }
            return sResult;
        }

    }
}
