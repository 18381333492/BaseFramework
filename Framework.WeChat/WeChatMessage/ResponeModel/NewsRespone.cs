using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat.WeChatMessage
{
    /// <summary>
    /// 图文消息实体
    /// </summary>
    public class NewsRespone:ISMessage
    {
         /// <summary>
         /// 无参构造函数
         /// </summary>
         public NewsRespone(){}

        /// <summary>
        /// 构造函数调用父类的
        /// </summary>
        public NewsRespone(IRMessage model) : base(model)
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
        } = "news";

        /// <summary>
        /// 图文消息个数，限制为8条以内
        /// </summary>
        public int ArticleCount
        {
            get;
            set;
        }

        /// <summary>
        /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过8，则将会无响应
        /// </summary>
        [CDATA]
        public List<item> Articles
        {
            get;
            set;
        } = new List<item>();
    }
}
