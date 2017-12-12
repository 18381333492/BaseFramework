using Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Utility.Models;
using Framework.Utility.Tools;

namespace Framework.Services
{
    public class UserServices:IUser
    {
        /// <summary>
        /// 分页获取管理员数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="iState">状态</param>
        /// <returns></returns>
        public override string GetList(PageInfo pageInfo,int? iState)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(@"select a.*,b.sRoleName,c.sText from ES_User a 
                                            left join ES_Role b on a.sRoleId=b.ID 
                                            left join ES_Dictionary c on a.iState=c.iValue and c.sValueType='UserState'
                                            where a.bIsDeleted=0");
            //创建动态参数
            dynamic parameters = new System.Dynamic.ExpandoObject();
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and (a.sName like @keyword or a.sPhone like @keyword or a.sNick like @keyword or a.sOpenId like @keyword )");
                parameters.keyword = string.Format("%{0}%", pageInfo.keyword);
            }
            if (iState != null)
            {
                sSql.Append(" and a.iState=@iState");
                parameters.iState = iState;
            }
            var res = query.PaginationQuery(sSql.ToString(), pageInfo, parameters);
            return JsonHelper.ToJsonString(res);
        }
    }
}
