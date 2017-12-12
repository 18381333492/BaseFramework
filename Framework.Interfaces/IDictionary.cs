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
    /// 数据字典接口
    /// </summary>
    public abstract class IDictionary: IBase
    {
        /// <summary>
        /// 分页获取数据字典列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract string GetList(PageInfo pageInfo);

        /// <summary>
        /// 根据字典类型获取ComboBox数据列表
        /// </summary>
        /// <param name="sType"></param
        /// <param name="bSelect">是否默认选中</param>
        /// <returns></returns>
        public abstract string GetComboBoxData(string sValueType, bool bSelect);

        /// <summary>
        /// 获取数据字典实体对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract ES_Dictionary Get(Guid ID);

        /// <summary>
        /// 添加数据字典
        /// </summary>
        /// <param name="Dictionary"></param>
        /// <returns></returns>
        public abstract int Insert(ES_Dictionary Dictionary);

        /// <summary>
        /// 编辑数据字典
        /// </summary>
        /// <param name="Dictionary"></param>
        /// <returns></returns>
        public abstract int Update(ES_Dictionary Dictionary);

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="DictionaryIds"></param>
        /// <returns></returns>
        public abstract int Cancel(string DictionaryIds);
    }
}
