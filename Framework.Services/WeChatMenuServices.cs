using Framework.Interfaces;
using Framework.Utility.Models;
using Framework.Utility.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Entity;
using Framework.DBAccess.EF;
using Framework.DBAccess.Dapper;

namespace Framework.Services
{
    public class WeChatMenuServices: IWeChatMenu
    {
        /// <summary>
        /// 获取微信公众号菜单分页数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public override string GetList(PageInfo pageInfo, string sWeChatId)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(@"select a.*,
                                (case when  a.sParentMenuId is null then '一级菜单'
			                     else  (select sMenuName from ES_WeChatMenu b where b.ID=a.sParentMenuId)
			                     end) sParentName
			                    from ES_WeChatMenu a where bIsDeleted=0");
            //创建动态参数
            SqlServerDbParameters Parameters = new SqlServerDbParameters();
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and sMenuName like @keyword");
                Parameters.Add("keyword", string.Format("%{0}%", pageInfo.keyword));
            }
            if (!string.IsNullOrEmpty(sWeChatId))
            {//公众号的过滤
                sSql.Append(" and sWeChatId=@sWeChatId");
                Parameters.Add("sWeChatId", sWeChatId);
            }
            var res = query.PaginationQuery(sSql.ToString(), pageInfo, Parameters);
            return JsonHelper.ToJsonString(res);
        }

        /// <summary>
        /// 根据主键ID获取微信公众号菜单实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override ES_WeChatMenu Get(Guid ID)
        {
            var item = query.Find<ES_WeChatMenu>(ID);
            return item;
        }

        /// <summary>
        /// 根据微信公众号获取相应的一级菜单
        /// </summary>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public override string GetFirstMenuByWeChat(string sWeChatId)
        {
            var list = query.QueryList<dynamic>(@"select ID,sMenuName from ES_WeChatMenu 
                                                            where bIsDeleted=0 and sWeChatId=@sWeChatId and sParentMenuId is null",new { sWeChatId= sWeChatId });
            list.Add(new { ID = string.Empty, sMenuName = "一级菜单" });
            return JsonHelper.ToJsonString(list);
        }

        /// <summary>
        /// 所有公众号的所有菜单
        /// </summary>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public override List<ES_WeChatMenu> GetMenuByWeChat(string sWeChatId)
        {
            var menuList = query.QueryList<ES_WeChatMenu>(@"select * from ES_WeChatMenu 
                                                                    where bIsDeleted=0 and sWeChatId=@sWeChatId order by iOrder asc", new { sWeChatId = sWeChatId });
            return menuList.ToList();
        }

        /// <summary>
        /// 添加公众号菜单
        /// </summary>
        /// <param name="WeChatMenu"></param>
        /// <returns></returns>
        public override int Insert(ES_WeChatMenu WeChatMenu)
        {
            using (var Context=new Entities())
            {
                WeChatMenu.ID = GuidHelper.NewGuid();
                WeChatMenu.dInsertTime = DateTime.Now;
                WeChatMenu.bIsDeleted = false;
                Context.Insert<ES_WeChatMenu>(WeChatMenu);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 编辑公众号菜单
        /// </summary>
        /// <param name="WeChatMenu"></param>
        /// <returns></returns>
        public override int Update(ES_WeChatMenu WeChatMenu)
        {
            using (var Context = new Entities())
            {
                Context.Update<ES_WeChatMenu>(WeChatMenu);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 删除公众号菜单
        /// </summary>
        /// <param name="sWeChatMenuIds"></param>
        /// <returns></returns>
        public override int Cancel(string sWeChatMenuIds)
        {
            using (var Context = new Entities())
            {
                var res=Context.Cancel<ES_WeChatMenu>(sWeChatMenuIds);
                return res;
            }
        }
    }
}
