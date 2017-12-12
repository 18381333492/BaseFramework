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
    /// 按钮接口
    /// </summary>
    public abstract class IButton: IBase
    {
        
        /// <summary>
        /// 获取按钮分页数据
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract string GetList(PageInfo pageInfo);

        /// <summary>
        /// 根据主键获取按钮实体
        /// </summary>
        /// <param name="ButtonId"></param>
        /// <returns></returns>
        public abstract ES_Button Get(Guid ButtonId);

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public abstract int Insert(ES_Button Button);

        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public abstract int Update(ES_Button Button);

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="ButtonIds"></param>
        /// <returns></returns>
        public abstract int Cancel(string ButtonIds);

    }
}
