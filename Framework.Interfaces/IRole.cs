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
    /// 角色菜单
    /// </summary>
    public abstract class IRole: IBase
    {

        /// <summary>
        /// 分页获取角色数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract string GetList(PageInfo pageInfo);

        /// <summary>
        /// 根据角色ID获取用户的权限列表
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public abstract List<dynamic> GetUserPower(object RoleId);

        /// <summary>
        /// 根据主键ID获取角色实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract ES_Role Get(Guid ID);

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public abstract int Insert(ES_Role Role);

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public abstract int Update(ES_Role Role);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleIds"></param>
        /// <returns></returns>
        public abstract int Cancel(string RoleIds);

        /// <summary>
        /// 权限分配
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public abstract int PowerSet(ES_Role Role);
    }
}
