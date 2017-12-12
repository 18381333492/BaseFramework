using Framework.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Models
{
    /// <summary>
    /// 分页参数实体
    /// </summary>
    public class PageInfo
    {

        /// <summary>
        /// 当前页索引
        /// </summary>
        public int page
        {
            get;
            set;
        } = 1;

        /// <summary>
        /// 页面数据的大小
        /// </summary>
        public int rows
        {
            get;
            set;
        } = 10;

        /// <summary>
        /// 排序的字段名称
        /// </summary>
        public string sort
        {
            get;
            set;
        } = "ID";

        /// <summary>
        /// 排序方式asc or desc
        /// </summary>
        public OrderType order
        {
            get;
            set;
        } = OrderType.DESC;

        /// <summary>
        /// 关键字
        /// </summary>
        public string keyword
        {
            get;
            set;
        }
    }
}
