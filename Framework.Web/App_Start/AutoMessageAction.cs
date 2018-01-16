using Framework.WeChat.WeChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.Interfaces;

namespace Framework.Web.App_Start
{
    /// <summary>
    /// 微信推送的消息事件的自动处理
    /// </summary>
    public class AutoMessageAction: IAction
    {
        /// <summary>
        /// 关注事件的处理
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override string HandleSubscribeEvent(SubscribeEvent message)
        {
            string result = string.Empty;
            var Manager = DI.DIEntity.Instance.GetImpl<IWeChatConcern>();
            //根据原始ID获取关注回复
            var concern = Manager.GetByOriginalId(message.ToUserName);
            if (concern.bIsConcernOn)
            {//开启关注回复
                if (concern.iConcernType ==1)
                {//文本回复
                    TextRespone model = new TextRespone(message);
                    model.Content = concern.sContent;
                    result = ActioningRespone.Serialize(model); 
                }
                if (concern.iConcernType == 2)
                {//图文回复
                    NewsRespone model = new NewsRespone(message);
                    model.Articles.Add(new item()
                    {
                        Title = "测试图文消息",
                        Description = "123456",
                        Url = "http://www.baidu.com",
                        PicUrl= "https://p1.ssl.qhimg.com/d/_hao360/weather/0.png"
                    });
                    model.Articles.Add(new item()
                    {
                        Title = "测试图文消息1",
                        Description = "555555",
                        Url = "http://www.baidu.com",
                        PicUrl = "https://p1.ssl.qhimg.com/d/_hao360/weather/0.png"
                    });
                    model.ArticleCount = model.Articles.Count;
                    result = ActioningRespone.Serialize(model);
                }
            }
            return result;
        }
    }
}