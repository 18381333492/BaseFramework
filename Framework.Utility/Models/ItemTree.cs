using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Models
{
    public class ItemTree
    {
        public string id
        {
            get;
            set;
        }

        public string pid
        {
            get;
            set;
        }

        public string text
        {
            get;
            set;
        }
            
        /// <summary>
        /// 自定义属性
        /// </summary>
        public object attributes
        {
            get;
            set;
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string iconCls
        {
            get;
            set;
        }  

        /// <summary>
        /// 节点状态
        /// </summary>
        public string state
        {
            get;
            set;
        }
    }
}
