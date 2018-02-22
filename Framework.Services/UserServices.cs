using Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Utility.Models;
using Framework.Utility.Tools;
using Framework.Entity;
using Framework.DBAccess.Dapper;
using System.Data;
using Framework.DBAccess.EF;

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
                                            where a.bIsDeleted=0 and a.bIsSuper=0");
            //创建动态参数
            SqlServerDbParameters Parameters = new SqlServerDbParameters();
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and (a.sName like @keyword or a.sPhone like @keyword or a.sNick like @keyword or a.sOpenId like @keyword )");
                Parameters.Add("keyword", string.Format("%{0}%", pageInfo.keyword));
            }
            if (iState != null)
            {
                sSql.Append(" and a.iState=@iState");
                Parameters.Add("iState", iState);
            }
            var res = query.PaginationQuery(sSql.ToString(), pageInfo, Parameters);
            return JsonHelper.ToJsonString(res);
        }

        /// <summary>
        /// 根据OpenId获取管理员信息
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public override ES_User Get(string sOpenId)
        {
            var user = query.SingleQuery<ES_User>(@"select * from ES_User where sOpenId=@sOpenId and bIsDeleted=0",new { sOpenId= sOpenId });
            return user;
        }

        /// <summary>
        /// 根据主键ID获取管理员信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public override ES_User Get(Guid UserID)
        {
            var user = query.Find<ES_User>(UserID);
            return user;
        }

        /// <summary>
        /// 管理员的登录操作
        /// </summary>
        /// <param name="UserId"></param>
        public override List<dynamic> LoginOperate(Guid UserId)
        {
            SqlServerDbParameters Parameters = new SqlServerDbParameters();
            Parameters.Add("@in_user_id", UserId.ToString());
            var ret = query.QueryProcedure<dynamic>("sp_login_operate", Parameters);
            return ret.ToList();
        }

        /// <summary>
        /// 管理员的审核
        /// </summary>
        /// <returns></returns>
        public override int Verify(Guid sUserId)
        {
            using (var Context=new Entities())
            {
                var user=Context.ES_User.Find(sUserId);
                user.iState = 1;
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 管理员的冻结/解冻操作
        /// </summary>
        /// <param name="sUserId"></param>
        /// <returns></returns>
        public override int Freeze(Guid sUserId)
        {
            using (var Context = new Entities())
            {
                var user = Context.ES_User.Find(sUserId);
                if (user.iState == 1)
                        user.iState = -1;
                else
                    user.iState = 1;
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 设置管理员角色
        /// </summary>
        /// <param name="sUserId"></param>
        /// <param name="sRoleId"></param>
        /// <returns></returns>
        public override int SetRole(Guid sUserId, string sRoleId)
        {
            using (var Context = new Entities())
            {
                var user = Context.ES_User.Find(sUserId);
                user.sRoleId = sRoleId;
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="sUserIds"></param>
        /// <returns></returns>
        public override int Cancel(string sUserIds)
        {
            using (var Context = new Entities())
            {
                return Context.Cancel<ES_User>(sUserIds);
            }
        }
    }
}
