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
            SqlServerDbParameters Parameters = new SqlServerDbParameters();
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and sRoleName like @sRoleName");
                Parameters.Add("sRoleName", string.Format("%{0}%", pageInfo.keyword));
            }
            var res = query.PaginationQuery(sSql.ToString(), pageInfo, Parameters);
            return JsonHelper.ToJsonString(res);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public override string GetRoleList()
        {
            var roleList = query.QueryList<ES_Role>("select ID,sRoleName from ES_Role where bIsDeleted=0");
            return JsonHelper.ToJsonString(roleList);
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
        public override int PowerSave(ES_Role Role)
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
