using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 处理微信消息的基类接口
    /// </summary>
    public abstract class IAction
    {
        #region 消息推送接口

        /// <summary>
        ///  文本消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HandleText(TextMessage message)
        {
            return string.Empty;
        }

        /// <summary>
        /// 图片消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HandleImage(ImageMessage message)
        {
            return string.Empty;
        }

        /// <summary>
        /// 链接消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HandleLink(LinkMessage message)
        {
            return string.Empty;
        }

        /// <summary>
        /// 位置消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HandleLocation(LocationMessage message)
        {
            return string.Empty;
        }

        /// <summary>
        /// 视频消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HandleVideo(VideoMessage message)
        {
            return string.Empty;
        }
        
        /// <summary>
        /// 语音消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HandleVoice(VoiceMessage message)
        {
            return string.Empty;
        }

        #endregion

        #region 事件推送的接口

        /// <summary>
        /// 关注时触发
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HandleSubscribeEvent(SubscribeEvent message)
        {
            return string.Empty;
        }

        /// <summary>
        /// 取消关注时触发
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HandleUnSubscribeEvent(UnSubscribeEvent message)
        {
            return string.Empty;
        }

        #endregion
    }
}
