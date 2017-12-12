using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Models
{
    /// <summary>
    /// 字典下拉列表的Item
    /// </summary>
    public class ItemDictionary
    {
        public string Name
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 下拉列表的值
        /// </summary>
        public int? Value
        {
            get;
            set;
        } = null;

        public int Width
        {
            get;
            set;
        } = 120;

        public string Required
        {
            get;
            set;
        } = "false";

        /// <summary>
        /// 是否默认选中
        /// </summary>
        public bool bSelect
        {
            get;
            set;
        } = false;
    }
}
