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
    /// 微信公众号接口
    /// </summary>
    public abstract class IWeChat: IBase
    {
        /// <summary>
        /// 获取微信公众号分页数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract string GetList(PageInfo pageInfo, int? iType);

        /// <summary>
        /// 获取微信公众号实体对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract ES_WeChat Get(Guid ID);

        /// <summary>
        /// 获取成为开发者的微信公众号
        /// </summary>
        /// <returns></returns>
        public abstract ES_WeChat GetDeveloper();

        /// <summary>
        /// 添加微信公众号
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public abstract int Insert(ES_WeChat WeChat);

        /// <summary>
        /// 编辑微信公众号
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public abstract int Update(ES_WeChat WeChat);

        /// <summary>
        /// 删除微信公众号
        /// </summary>
        /// <param name="WeChatIds"></param>
        /// <returns></returns>
        public abstract int Cancel(string WeChatIds);

        /// <summary>
        /// 成为开发者
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract int Developer(Guid ID);
    }
}
