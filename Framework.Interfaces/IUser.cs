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
    }
}
