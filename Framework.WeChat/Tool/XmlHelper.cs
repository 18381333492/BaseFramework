using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Framework.WeChat.Tool
{
    public class XmlHelper
    {
        /// <summary>
        /// XML数据的反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sXmlContent"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string sXmlContent)
        {
            sXmlContent = Regex.Replace(sXmlContent, "xml", typeof(T).Name);
            StringReader stringReader = new StringReader(sXmlContent);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var item=serializer.Deserialize(xmlReader);
            return (T)item;
        }

        /// <summary>
        /// 将对象序列化成XMl数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Serialize(object model)
        {
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");
            //在Serialize对象时，传入一个XmlSerializerNamespaces，
            //这样XmlSerializer就会用你传入的XmlSerializerNamespaces而不会用default的XmlSerializerNamespaces。
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = true;
            xmlWriterSettings.Indent = true;//一个元素一个新行
            //去掉默认Version信息
            XmlSerializer xs = new XmlSerializer(model.GetType());
            StringWriter sw = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(sw, xmlWriterSettings))
            {
                xs.Serialize(xmlWriter, model, xmlSerializerNamespaces);
                string result = sw.ToString();
                result = Regex.Replace(result, model.GetType().Name, "xml");
                result = HandleSpecialChar(result);//处理特殊字符
                return result;
            }
        }

        /// <summary>
        /// XMl数据特殊字符处理
        /// </summary>
        /// <returns></returns>
        public static string HandleSpecialChar(string sContent)
        {
            sContent = Regex.Replace(sContent, "&lt;", "<");
            sContent = Regex.Replace(sContent, "&gt;", ">");
            sContent = Regex.Replace(sContent, "&amp;", "&");
            sContent = Regex.Replace(sContent, "&apos;", "'");
            sContent = Regex.Replace(sContent, "&quot;", "\"");
            return sContent;
        }

        /// <summary>
        /// 根据名字获取对应的值
        /// </summary>
        /// <param name="sXmlContent"></param>
        /// <param name="sName"></param>
        /// <returns></returns>
        public static string getTextByNode(string sXmlContent, string sName)
        {
            XElement xElement = XElement.Parse(sXmlContent);
            var xResultElement = xElement.Element(sName);
            return xResultElement == null ? string.Empty : xResultElement.Value;
        }
    }
}
