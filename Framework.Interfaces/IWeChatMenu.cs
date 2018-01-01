using Framework.Entity;
using Framework.Utility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    /// <summary>
    /// 微信公众号菜单接口
    /// </summary>
    public abstract class IWeChatMenu:IBase
    {
        /// <summary>
        /// 获取微信公众号菜单分页数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public abstract string GetList(PageInfo pageInfo,string sWeChatId);

        /// <summary>
        /// 根据主键ID获取微信公众号菜单实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract ES_WeChatMenu Get(Guid ID);

        /// <summary>
        /// 根据微信公众号获取相应的一级菜单
        /// </summary>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public abstract string GetFirstMenuByWeChat(string sWeChatId);

        /// <summary>
        /// 所有公众号的所有菜单
        /// </summary>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public abstract List<ES_WeChatMenu> GetMenuByWeChat(string sWeChatId);

        /// <summary>
        /// 添加公众号菜单
        /// </summary>
        /// <param name="WeChatMenu"></param>
        /// <returns></returns>
        public abstract int Insert(ES_WeChatMenu WeChatMenu);

        /// <summary>
        /// 编辑公众号菜单
        /// </summary>
        /// <param name="WeChatMenu"></param>
        /// <returns></returns>
        public abstract int Update(ES_WeChatMenu WeChatMenu);

        /// <summary>
        /// 删除公众号菜单
        /// </summary>
        /// <param name="sWeChatMenuIds"></param>
        /// <returns></returns>
        public abstract int Cancel(string sWeChatMenuIds);
    }
}
