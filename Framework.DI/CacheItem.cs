using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DI
{
    public class CacheItem
    {
        //接口名称
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 接口的实现
        /// </summary>
        public object Item
        {
            get;
            set;
        }
        
    }
}
