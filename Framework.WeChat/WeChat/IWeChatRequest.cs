using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{
    /// <summary>
    /// 微信请求的接口
    /// </summary>
    public interface IWeChatRequest
    {

        /// <summary>
        /// 执行微信请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        T Execute<T>(WeChatModel model); 
    }
}
