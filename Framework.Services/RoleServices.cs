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
    public class RoleServices:IRole
    {
        /// <summary>
        /// 分页获取角色数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override string GetList(PageInfo pageInfo)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("select * from ES_Role where bIsDeleted=0");
            //创建动态参数
            dynamic parameters = new System.Dynamic.ExpandoObject();
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and sRoleName like @sRoleName");
                parameters.sRoleName =string.Format("%{0}%", pageInfo.keyword);
            }
            var res = query.PaginationQuery(sSql.ToString(), pageInfo, parameters);
            return JsonHelper.ToJsonString(res);
        }

        /// <summary>
        /// 根据角色ID获取用户的权限列表
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public override List<dynamic> GetUserPower(object RoleId)
        {
            query.QueryList<dynamic>("select * from ES_Role where ID=@RoleId");
            return null;
        }

        /// <summary>
        /// 根据主键ID获取角色实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override ES_Role Get(Guid ID)
        {
            var obj = query.Find<ES_Role>(ID);
            return obj;
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public override int Insert(ES_Role Role)
        {
            using (var Context=new Entities())
            {
                Role.ID = GuidHelper.NewGuid();
                Role.dInsertTime = DateTime.Now;
                Context.Insert<ES_Role>(Role);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public override int Update(ES_Role Role)
        {
            using (var Context = new Entities())
            {
                var obj = Context.ES_Role.Find(Role.ID);
                obj.sRoleName = Role.sRoleName;
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleIds"></param>
        /// <returns></returns>
        public override int Cancel(string RoleIds)
        {
            using (var Context = new Entities())
            {
                return Context.Cancel<ES_Role>(RoleIds);
            }
        }

        /// <summary>
        /// 权限分配
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public override int PowerSet(ES_Role Role)
        {
            using (var Context = new Entities())
            {
                var obj = Context.ES_Role.Find(Role.ID);
                obj.sPowerIds = Role.sPowerIds;
                return Context.SaveExtendChanges();
            }
        }

    }
}
