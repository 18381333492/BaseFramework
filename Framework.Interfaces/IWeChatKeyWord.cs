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
    /// 公众号关键字接口
    /// </summary>
    public abstract class IWeChatKeyWord : IBase
    {
        /// <summary>
        /// 获取公众号关键字分页数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public abstract string GetList(PageInfo pageInfo,string sWeChatId);

        /// <summary>
        /// 根据主键ID获取公众号关键字实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract ES_WeChatKeyWord Get(Guid ID);

        /// <summary>
        /// 添加公众号关键字
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public abstract int Insert(ES_WeChatKeyWord KeyWord);

        /// <summary>
        /// 编辑公众号关键字
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public abstract int Update(ES_WeChatKeyWord KeyWord);

        /// <summary>
        /// 删除公众号关键字
        /// </summary>
        /// <param name="sWeChatKeyWordIds"></param>
        /// <returns></returns>
        public abstract int Cancel(string sWeChatKeyWordIds);
    }
}
