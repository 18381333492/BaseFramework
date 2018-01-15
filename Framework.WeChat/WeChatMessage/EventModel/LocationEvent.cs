using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 微信发送上报地理位置事件的实体
    /// </summary>
    public class LocationEvent:IRMessage
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Longitude
        {
            get;
            set;
        }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Precision
        {
            get;
            set;
        }
    }
}
