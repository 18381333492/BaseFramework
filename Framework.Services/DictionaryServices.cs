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
using Framework.DBAccess.Dapper;

namespace Framework.Services
{
    public class DictionaryServices: IDictionary
    {
        /// <summary>
        /// 分页获取数据字典列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override string GetList(PageInfo pageInfo)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("select * from ES_Dictionary where bIsDeleted=0");
            //创建动态参数
            SqlServerDbParameters Parameters = new SqlServerDbParameters();
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and sText like @sText");
                Parameters.Add("sText", string.Format("%{0}%", pageInfo.keyword));
            }
            var res = query.PaginationQuery(sSql.ToString(), pageInfo, Parameters);
            return JsonHelper.ToJsonString(res);
        }

        /// <summary>
        /// 根据字典类型获取ComboBox数据列表
        /// </summary>
        /// <param name="sType"></param
        /// <param name="bSelect">是否默认选中</param>
        /// <returns></returns>
        public override string GetComboBoxData(string sValueType,bool bSelect)
        {
            var res = query.QueryList<dynamic>(@"select sText,iValue from ES_Dictionary 
                                                                     where bIsDeleted=0 and sValueType=@sValueType order by iOrder asc", new { sValueType= sValueType });
            if(bSelect) res.Insert(0, new { sText = "----请选择----", iValue = string.Empty, selected = true });
            return JsonHelper.ToJsonString(res);
        }

        /// <summary>
        /// 获取数据字典实体对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override ES_Dictionary Get(Guid ID)
        {
            var obj = query.Find<ES_Dictionary>(ID);
            return obj;
        }

        /// <summary>
        /// 添加数据字典
        /// </summary>
        /// <param name="Dictionary"></param>
        /// <returns></returns>
        public override int Insert(ES_Dictionary Dictionary)
        {
            using (var Context = new Entities())
            {
                Dictionary.ID = GuidHelper.NewGuid();
                Context.Insert<ES_Dictionary>(Dictionary);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 编辑数据字典
        /// </summary>
        /// <param name="Dictionary"></param>
        /// <returns></returns>
        public override int Update(ES_Dictionary Dictionary)
        {
            using (var Context = new Entities())
            {
                Context.Update<ES_Dictionary>(Dictionary);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="DictionaryIds"></param>
        /// <returns></returns>
        public override int Cancel(string DictionaryIds)
        {
            using (var Context = new Entities())
            {
                return Context.Cancel<ES_Dictionary>(DictionaryIds);
            }
        }
    }
}
