using Framework.DBAccess.Dapper;
using Framework.DBAccess.EF;
using Framework.Entity;
using Framework.Interfaces;
using Framework.Utility.Models;
using Framework.Utility.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Services
{
    public class ButtonServices:IButton
    {
        /// <summary>
        /// 获取按钮分页数据
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override string GetList(PageInfo pageInfo)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("select * from ES_Button where bIsDeleted=0");
            //创建动态参数
            SqlServerDbParameters Parameters = new SqlServerDbParameters();
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and sMenuId=@sMenuId");
                Parameters.Add("sMenuId", pageInfo.keyword);
            }
            var res= query.PaginationQuery(sSql.ToString(), pageInfo, Parameters);
            return JsonHelper.ToJsonString(res);
        }


        /// <summary>
        /// 根据主键获取按钮实体
        /// </summary>
        /// <param name="ButtonId"></param>
        /// <returns></returns>
        public override ES_Button Get(Guid ButtonId)
        {
            var obj = query.Find<ES_Button>(ButtonId);
            return obj;
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public override int Insert(ES_Button Button)
        {
            using (var Context=new Entities())
            {
                Button.ID = GuidHelper.NewGuid();
                Button.bIsDeleted = false;
                Context.Insert<ES_Button>(Button);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public override int Update(ES_Button Button)
        {
            using (var Context = new Entities())
            {
                Context.Update<ES_Button>(Button);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="ButtonIds"></param>
        /// <returns></returns>
        public override int Cancel(string ButtonIds)
        {
            using (var Context = new Entities())
            {
                return Context.Cancel<ES_Button>(ButtonIds);
            }
        }
    }
}
