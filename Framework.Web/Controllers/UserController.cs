using Framework.Interfaces;
using Framework.Utility.Models;
using Framework.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.WeChat;
using System.Text;
using Framework.Utility.Enums;
using Framework.Entity;

namespace Framework.Web.Controllers
{
    /// <summary>
    /// 管理员控制器
    /// </summary>
    public class UserController : BaseController<IUser>
    {
        //
        // GET: /User/

        /// <summary>
        /// 分页获取管理员数据视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PageInfo pageInfo,int? iState)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                return Content(Manager.GetList(pageInfo, iState));
            }
        }

        /// <summary>
        /// 登录过期页面
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult TimeOut()
        {
            return View();
        }

        /// <summary>
        /// 后台管理员登录
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult Login()
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                var user = Manager.Get("omHVhuM92qR97IBMffkx6smEoZjc");
                CacheUserInfo(user);
                return View();
            }
            else
            {
                return Json(null);
            }
        }

        /// <summary>
        /// 微信确认登录
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult WeChatConfirm()
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                string sSocketId = Request.QueryString["sSocketId"];
                string code = Request.QueryString["code"];
                var manageWeChat = GetImpl<IWeChat>();
                var weChat = manageWeChat.GetDeveloper();//获取开发者微信公众号
                //if (string.IsNullOrEmpty(code))
                //{
                //    //网页授权获取用户信息
                //    string sCurrentUrl = string.Format("http://{0}{1}",Request.Url.Host, Request.Url.PathAndQuery);
                //    string sUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
                //    sUrl = string.Format(sUrl, weChat.sAppId, HttpUtility.UrlEncode(sCurrentUrl, Encoding.UTF8));
                //    return Redirect(sUrl);
                //}
                //else
                //{//利用code获取用户信息
                //    IWeChatRequest request = new GetWeChatUserInfoByPageAuthorizeRequest();
                //    GetWeChatUserInfoByPageAuthorizeModel model = new GetWeChatUserInfoByPageAuthorizeModel();
                //    model.sAppId = weChat.sAppId;
                //    model.sAppSecret = weChat.sAppSecret;
                //    model.sCode = code;
                //    var respone=request.Execute<GetWeChatUserInfoByPageAuthorizeRespone>(model);
                //    ViewBag.Nick = respone.nickname;
                //    ViewBag.Head = respone.headimgurl;
                //    ViewBag.sSocketId = sSocketId;
                //    ViewBag.sOpenId = respone.openid;
                //}
                ViewBag.sSocketId = sSocketId;
                return View();
            }
            else
            {
                string sOpenId = Request.Form["sOpenId"];
                var user = Manager.Get(sOpenId);
                if (user != null)
                {
                    if (user.iState == 1)
                    {//状态正常,登录成功
                        result.success = true;
                        CacheUserInfo(user);
                    }
                    else
                    {
                        result.success = false;
                        result.info = user.iState == -1 ? "该账户已被冻结,请联系管理员" : "该账户正在审核中,请联系管理员";
                    }       
                }
                else
                {
                    result.success = false;
                    result.info = "该账户不存在,请联系管理员";
                }
                return Json(result);
            }
        }

        /// <summary>
        /// 审核管理员
        /// </summary>
        /// <param name="sUserId"></param>
        /// <returns></returns>
        public ActionResult Verify(Guid sUserId)
        {
            if (Manager.Verify(sUserId) > 0)
                result.success = true;
            return Json(result);
        }

        /// <summary>
        /// 冻结/解冻管理员
        /// </summary>
        /// <param name="sUserId"></param>
        /// <returns></returns>
        public ActionResult Freeze(Guid sUserId)
        {
            if (Manager.Freeze(sUserId) >0)
                result.success = true;
            return Json(result);
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="sUserId"></param>
        /// <param name="sRoleId"></param>
        /// <returns></returns>
        public ActionResult SetRole(Guid sUserId,string sRoleId)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                ViewBag.RoleID = Manager.Get(sUserId).sRoleId;
                ViewBag.sUserId = sUserId;
                return View();
            }
            else
            {
                if (Manager.SetRole(sUserId, sRoleId) >= 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="sUserIds"></param>
        /// <returns></returns>
        public ActionResult Cancel(string sUserIds)
        {
            if (Manager.Cancel(sUserIds) > 0)
                result.success = true;
            return Json(result);
        }

        /// <summary>
        /// 缓存用户相关的信息
        /// </summary>
        /// <param name="user"></param>
        [NoLogin]
        private void CacheUserInfo(ES_User user)
        {
            //缓存管理员信息
            Session[SessionType.USER] = new LoginCacheInfo()
            {
                ID = user.ID,
                sName = user.sName,
                sHeadImg=user.sHeadPicture
            };
            //管理员登录操作
            var powerList =Manager.LoginOperate(user.ID);
            var menuList = powerList.Where(m => m.iType == 1);
            var btnList = powerList.Where(m => m.iType == 2);
            Session[SessionType.MENU] = menuList;
            Session[SessionType.BTN] = btnList;
        }
    }
}
