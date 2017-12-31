using Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Utility.Tools;
using Framework.Utility.Enums;

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

            }
        }

    }
}
