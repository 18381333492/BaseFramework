using Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Utility.Models;
using Framework.Entity;
using Framework.Utility.Tools;
using Framework.DBAccess.EF;

namespace Framework.Services
{
    public class WeChatServices: IWeChat
    {
        /// <summary>
        /// 获取微信公众号分页数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override string GetList(PageInfo pageInfo, int? iType)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(@"select a.*,b.sText from ES_WeChat a left join ES_Dictionary b
                                                              on a.iType=b.iValue and b.sValueType='WeChatType'
                                                              where a.bIsDeleted=0");
            //创建动态参数
            dynamic parameters = new System.Dynamic.ExpandoObject();
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and a.sWeChatName like @sWeChatName");
                parameters.sWeChatName = string.Format("%{0}%", pageInfo.keyword);
            }
            if (iType != null)
            {
                sSql.Append(" and a.iType=@iType");
                parameters.iType = iType;
            }
            var res = query.PaginationQuery(sSql.ToString(), pageInfo, parameters);
            return JsonHelper.ToJsonString(res);
        }

        /// <summary>
        /// 获取微信公众号实体对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override ES_WeChat Get(Guid ID)
        {
            var obj = query.Find<ES_WeChat>(ID);
            return obj;
        }

        /// <summary>
        /// 获取成为开发者的微信公众号
        /// </summary>
        /// <returns></returns>
        public override ES_WeChat GetDeveloper()
        {
            var obj = query.SingleQuery<ES_WeChat>("select * from ES_WeChat where bIsDeleted=0 and bIsDeveloper=1");
            return obj;
        }

        /// <summary>
        /// 添加微信公众号
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public override int Insert(ES_WeChat WeChat)
        {
            using (var Context=new Entities())
            {
                WeChat.ID = GuidHelper.NewGuid();
                WeChat.dInsertTime = DateTime.Now;
                WeChat.dUpdateTime = DateTime.Now;
                Context.Insert<ES_WeChat>(WeChat);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 编辑微信公众号
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public override int Update(ES_WeChat WeChat)
        {
            using (var Context = new Entities())
            {
                WeChat.dUpdateTime = DateTime.Now;
                Context.Update<ES_WeChat>(WeChat);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 删除微信公众号
        /// </summary>
        /// <param name="WeChatIds"></param>
        /// <returns></returns>
        public override int Cancel(string WeChatIds)
        {
            using (var Context = new Entities())
            {
                return Context.Cancel<ES_WeChat>(WeChatIds);
            }
        }

        /// <summary>
        /// 成为开发者
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override int Developer(Guid ID)
        {
            using(var Context=new Entities())
            {
                foreach(var item in Context.ES_WeChat)
                {
                    if (item.ID.ToString() == ID.ToString())
                        item.bIsDeveloper = true;
                    else
                        item.bIsDeveloper = false;
                    Context.Update<ES_WeChat>(item);
                }
                return Context.SaveExtendChanges();
            }
        }
    }
}
