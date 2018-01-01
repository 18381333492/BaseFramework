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
    /// 管理员接口
    /// </summary>
    public abstract class IUser:IBase
    {
        /// <summary>
        /// 分页获取管理员数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="iState">状态</param>
        /// <returns></returns>
        public abstract string GetList(PageInfo pageInfo, int? iState);

        /// <summary>
        /// 根据OpenId获取管理员信息
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public abstract ES_User Get(string sOpenId);

        /// <summary>
        /// 根据主键ID获取管理员信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public abstract ES_User Get(Guid UserID);

        /// <summary>
        /// 管理员的登录操作
        /// </summary>
        /// <param name="UserId"></param>
        public abstract List<dynamic> LoginOperate(Guid UserId);

        /// <summary>
        /// 管理员的审核
        /// </summary>
        /// <returns></returns>
        public abstract int Verify(Guid sUserId);

        /// <summary>
        /// 管理员的冻结/解冻操作
        /// </summary>
        /// <param name="sUserId"></param>
        /// <returns></returns>
        public abstract int Freeze(Guid sUserId);

        /// <summary>
        /// 设置管理员角色
        /// </summary>
        /// <param name="sUserId"></param>
        /// <param name="sRoleId"></param>
        /// <returns></returns>
        public abstract int SetRole(Guid sUserId, string sRoleId);

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="sUserIds"></param>
        /// <returns></returns>
        public abstract int Cancel(string sUserIds);
    }
}
