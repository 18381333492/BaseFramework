using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{
    /// <summary>
    /// 创建自定义菜单所需参数的Model
    /// </summary>
    public class CreateAutoMenuModel: WeChatModel
    {
        /// <summary>
        /// 菜单的json数据
        /// </summary>
        public string sBody { get; set; }
    }
}
