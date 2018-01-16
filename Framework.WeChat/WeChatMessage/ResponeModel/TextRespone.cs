using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 文本消息实体
    /// </summary>
    public class TextRespone: ISMessage
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TextRespone(){}

        /// <summary>
        /// 构造函数调用父类的
        /// </summary>
        public TextRespone(IRMessage model) : base(model)
        {

        }

        /// <summary>
        /// 响应的消息类型
        /// </summary>
        [CDATA]
        public string MsgType
        {
            get;
            set;
        } = "text";

        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
        /// </summary>
        [CDATA]
        public string Content
        {
            get;
            set;
        }
    }
}
