using Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    /// <summary>
    /// 微信公众号关注回复接口
    /// </summary>
    public abstract class IWeChatConcern:IBase
    {
        /// <summary>
        /// 根据微信公众号ID获取关注回复
        /// </summary>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public abstract ES_WeChatConcern Get(string sWeChatId);
    }
}
