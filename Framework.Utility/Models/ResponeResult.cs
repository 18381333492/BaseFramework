using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Models
{
    public class ResponeResult
    {

        /// <summary>
        /// 本次请求的成功的标识
        /// </summary>
        public bool success
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 消息描述
        /// </summary>
        public string info
        {
            get;
            set;
        } = "操作成功";


        /// <summary>
        /// 是否登录的标识
        /// </summary>
        public bool login
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object data
        {
            get;
            set;
        }
    }
}
