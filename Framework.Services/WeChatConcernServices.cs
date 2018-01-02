using Framework.Entity;
using Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Utility.Tools;
using Framework.DBAccess.EF;

namespace Framework.Services
{
    public class WeChatConcernServices: IWeChatConcern
    {
        /// <summary>
        /// 根据微信公众号ID获取关注回复
        /// </summary>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public override ES_WeChatConcern Get(string sWeChatId)
        {
            var concern = query.SingleQuery<ES_WeChatConcern>(@"select * from ES_WeChatConcern where sWeChatId=@sWeChatId", new { sWeChatId = sWeChatId });
            if (concern == null)
                concern = new ES_WeChatConcern();
            return concern;
        }


        /// <summary>
        /// 保存关注回复
        /// </summary>
        /// <param name="WeChatConcern"></param>
        /// <returns></returns>
        public override int Save(ES_WeChatConcern WeChatConcern)
        {
            using (var Context=new Entities())
            {
                if (WeChatConcern.ID == Guid.Empty)
                {//添加
                    WeChatConcern.ID = GuidHelper.NewGuid();
                    Context.Insert<ES_WeChatConcern>(WeChatConcern);
                    return Context.SaveExtendChanges();

                }
                else
                {//编辑
                    Context.Update<ES_WeChatConcern>(WeChatConcern);
                    var res=Context.SaveExtendChanges();
                    return res;
                }
            }
        }
    }
}
