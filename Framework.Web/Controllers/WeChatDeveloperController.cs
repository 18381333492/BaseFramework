using Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Utility.Tools;
using Framework.Utility.Enums;
using Framework.Web.App_Start;
using System.IO;
using System.Text;
using Framework.WeChat.WeChatMessage;

namespace Framework.Web.Controllers
{
    /// <summary>
    /// 微信开发者控制器
    /// 校验成为微信开发者
    /// </summary>
    public class WeChatDeveloperController : Controller
    {
        //
        // GET: /WeChatDeveloper/

        /// <summary>
        /// 处理微信推送的消息和事件
        /// </summary>
        private static ActioningRequest handle = new ActioningRequest(new AutoMessageAction());

        /// <summary>
        /// 微信公众号成为开发者接口
        /// </summary>
        public void Receive()
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                string result = string.Empty;
                var Manager = DI.DIEntity.Instance.GetImpl<IWeChat>();
                var WeChat = Manager.GetDeveloper();
                if (WeChat != null)
                {
                    string signature = Request.QueryString["signature"];
                    string timestamp = Request.QueryString["timestamp"];
                    string nonce = Request.QueryString["nonce"];
                    string echostr = Request.QueryString["echostr"];
                    string token = WeChat.sToken;
                    string[] array = { token, timestamp, nonce };
                    Array.Sort(array);//字典排序
                    string newSignature =SecurityHelper.SHA1(string.Join("", array));
                    if (newSignature == signature.ToUpper())
                        result=echostr;
                    else
                        result="Valiate Fail";
                }
                else
                    result = "token error";
                Response.Write(result);
            }
            else
            {//接受微信消息
                string result = HandleMessage(Request);
            }
        }

     
        /// <summary>
        /// 处理微信推送过来的消息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string HandleMessage(HttpRequestBase request)
        {
            StreamReader sr = new StreamReader(request.InputStream, Encoding.UTF8);
            string requestXmlMessage = sr.ReadToEnd();

            // 获取微信发送来的消息类型
            string sMsgType =Framework.WeChat.Tool.XmlHelper.getTextByNode(requestXmlMessage, "MsgType");

            if (sMsgType.ToUpper() == "EVENT")
            {//事件的推送
                string EventType = Framework.WeChat.Tool.XmlHelper.getTextByNode(requestXmlMessage, "Event");//获取事件类型
                EventType eventType = (EventType)Enum.Parse(typeof(EventType), EventType.ToUpper());
                return handle.ProcessEvent(eventType, requestXmlMessage);
            }
            else
            {//消息的推送
                // 找到对应的消息类型
                MsgType msgType = (MsgType)Enum.Parse(typeof(MsgType), sMsgType.ToUpper());
                return handle.ProcessMessage(msgType, requestXmlMessage);
            }
        }
    }
}
