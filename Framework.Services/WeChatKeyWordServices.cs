using Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Entity;
using Framework.Utility.Models;
using Framework.Utility.Tools;
using Framework.DBAccess.EF;
using Framework.DBAccess.Dapper;

namespace Framework.Services
{
    /// <summary>
    /// 公众号关键字逻辑业务处理
    /// </summary>
    public class WeChatKeyWordServices : IWeChatKeyWord
    { 
        /// <summary>
        /// 获取公众号关键字分页数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public override string GetList(PageInfo pageInfo, string sWeChatId)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("select * from ES_WeChatKeyWord where 1=1");
            //创建动态参数
            SqlServerDbParameters Parameters = new SqlServerDbParameters();
            if (!string.IsNullOrEmpty(sWeChatId))
            {
                sSql.Append(" and sWeChatId=@sWeChatId");
                Parameters.Add("sWeChatId", sWeChatId);
            }
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and sKeyWordName like @keyword");
                Parameters.Add("keyword", string.Format("%{0}%", pageInfo.keyword));
            }
            var res = query.PaginationQuery(sSql.ToString(), pageInfo, Parameters);
            return JsonHelper.ToJsonString(res);
        }

        /// <summary>
        /// 根据主键ID获取公众号关键字实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override ES_WeChatKeyWord Get(Guid ID)
        {
            var Item = query.Find<ES_WeChatKeyWord>(ID);
            return Item;
        }

        /// <summary>
        /// 添加公众号关键字
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public override int Insert(ES_WeChatKeyWord KeyWord)
        {
            using (var db=new Entities())
            {
                KeyWord.ID = GuidHelper.NewGuid();
                KeyWord.dInsertTime = DateTime.Now;
                KeyWord.dUpdateTime = DateTime.Now;
                db.Insert<ES_WeChatKeyWord>(KeyWord);
                return db.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 编辑公众号关键字
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public override int Update(ES_WeChatKeyWord KeyWord)
        {
            using (var db = new Entities())
            {
                KeyWord.dUpdateTime = DateTime.Now;
                db.Update<ES_WeChatKeyWord>(KeyWord);
                return db.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 删除公众号关键字
        /// </summary>
        /// <param name="sWeChatKeyWordIds"></param>
        /// <returns></returns>
        public override int Cancel(string sWeChatKeyWordIds)
        {
            using (var db=new Entities())
            {
                var res=db.Delete<ES_WeChatKeyWord>(sWeChatKeyWordIds);
                return res;
            }
        }
    }
}
