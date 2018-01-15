using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage.Action
{
    /// <summary>
    /// 对微信消息响应的封装
    /// </summary>
    public class ActioningRespone
    {
        /// <summary>
        /// 序列化成字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string Serialize<T>() where T : class, new()
        {
            return string.Empty;
        }
    }
}
