using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 微信发送地理位置消息的实体
    /// </summary>
    public class LocationMessage:IRMessage
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_X
        {
            get;
            set;
        }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y
        {
            get;
            set;
        }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale
        {
            get;
            set;
        }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label
        {
            get;
            set;
        }
    }
}
