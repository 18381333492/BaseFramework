using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.WeChat
{
    /// <summary>
    /// 获取微信用户信息的响应
    /// </summary>
    public class GetWeChatUserInfoRespone:WeChatRespone
    {
        /// <summary>
        /// 是否关注 0-关注 1-已关注
        /// </summary>
        public int subscribe { get; set; }

        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，
        /// 值为2时是女性，值为0时是未知
        /// </summary>
        public int sex { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string headimgurl { get; set; }

        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public long subscribe_time { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string unionid { get; set; }

        /// <summary>
        /// 公众号运营者对粉丝的备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 用户所在的分组ID（兼容旧的用户分组接口）
        /// </summary>
        public string groupid { get; set; }

        /// <summary>
        /// 用户被打上的标签ID列表
        /// </summary>
        public List<string> tagid_list { get; set; }
    }
}
