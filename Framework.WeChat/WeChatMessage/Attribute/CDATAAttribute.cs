using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{

    /// <summary>
    /// 自定义特性(需要序列化成CDATA数据格式XMl)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class CDATAAttribute : Attribute
    {
    }
}
