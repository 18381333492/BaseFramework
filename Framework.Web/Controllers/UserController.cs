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
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                return View();
            }
            else
            {
                return Content(Manager.GetList(pageInfo, iState));
            }
        }

        /// <summary>
        /// 后台管理员登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                var manageWeChat = GetImpl<IWeChat>();
                var weChat = manageWeChat.GetDeveloper();//获取开发者微信公众号
                IWeChatRequest request = new GetWeChatUserInfoRequest();
                GetWeChatUserInfoModel model = new GetWeChatUserInfoModel();
                model.sAppId = weChat.sAppId;
                model.sAppSecret = weChat.sAppSecret;
                model.sOpenId = "omHVhuM92qR97IBMffkx6smEoZjc";
                var respone=request.Execute<GetWeChatUserInfoRespone>(model);

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
        public ActionResult WeChatConfirm()
        {
            if (Request.HttpMethod.ToUpper() == "GET")
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
                return Json(null);
            }
        }

        /// <summary>
        /// 缓存用户相关的信息
        /// </summary>
        /// <param name="user"></param>
        private void CacheUserInfo(ES_User user)
        {
            Session[SessionType.USER] = new LoginCacheInfo()
            {
                ID = user.ID,
                sName = user.sName,
                sHeadImg=user.sHeadPicture
            };
            




        }
    }
}
