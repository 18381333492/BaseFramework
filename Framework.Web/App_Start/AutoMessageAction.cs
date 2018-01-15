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
            var Manager = DI.DIEntity.Instance.GetImpl<IWeChatConcern>();
            //根据原始ID获取关注回复
            var concern = Manager.GetByOriginalId(message.ToUserName);
            if (concern.bIsConcernOn)
            {//开启关注回复
                if (concern.iConcernType ==1)
                {//文本回复

                }
                if (concern.iConcernType == 2)
                {//图文回复

                }
            }
            return base.HandleSubscribeEvent(message);
        }
    }
}