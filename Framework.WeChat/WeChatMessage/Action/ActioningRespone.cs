using Framework.WeChat.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
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
        public static string Serialize(object model)
        {
            Type t = model.GetType();
            PropertyInfo[] PropertyArray=t.GetProperties(); 
            foreach(var Property in PropertyArray)
            {
                if (Property.GetCustomAttribute(typeof(CDATAAttribute)) != null)
                {//是否有CDATA特性
                    if (Property.PropertyType == typeof(List<item>))
                    {
                        List<item> Articles = Property.GetValue(model) as List<item>;
                        foreach (var item in Articles)
                        {
                            item.Title = string.Format("<![CDATA[{0}]]>", item.Title);
                            item.Description = string.Format("<![CDATA[{0}]]>", item.Description);
                            item.PicUrl = string.Format("<![CDATA[{0}]]>", item.PicUrl);
                            item.Url = string.Format("<![CDATA[{0}]]>", item.Url);
                        }
                        Property.SetValue(model, Articles);
                    }
                    else
                    {
                        string Value = Property.GetValue(model).ToString();
                        Value = string.Format("<![CDATA[{0}]]>", Value);
                        Property.SetValue(model, Value);
                    }
                }
            }
            string result = XmlHelper.Serialize(model);
            return result;

        }
    
    }
}
