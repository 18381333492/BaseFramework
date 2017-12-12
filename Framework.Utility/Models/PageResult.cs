using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Models
{
    /// <summary>
    /// 分页结果实体Model
    /// </summary>
    public class PageResult
    {
        /// <summary>
        /// 当前页码数
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 数据总数
        /// </summary>
        public int total { get; set; } = 0;

        /// <summary>
        /// 分页的结果
        /// </summary>
        public object rows { get; set; }

        /// <summary>
        /// 其它数据
        /// </summary>
        public object data { get; set; }

    }
}
