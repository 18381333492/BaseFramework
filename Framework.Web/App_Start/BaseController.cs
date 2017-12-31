using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.DI;
using Framework.Utility.Enums;
using Framework.Utility.Models;

namespace Framework.Web.App_Start
{
    public class BaseController<T>:Controller
    {
        //接口
        protected T Manager{ get;}

        /// <summary>
        /// 返回的响应结果
        /// </summary>
        protected ResponeResult result { get; set; }
        
        /// <summary>
        /// 管理员的登录信息
        /// </summary>
        protected LoginCacheInfo LoginStatus
        {
            get
            {
                return (LoginCacheInfo)Session[SessionType.USER];
            }
        }

        /// <summary>
        /// 构造函数实例化接口
        /// </summary>
        public BaseController()
        {
            Manager = GetImpl<T>();
            result = new ResponeResult();
        }

        /// <summary>
        /// 映射实现接口
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <returns></returns>
        protected static M GetImpl<M>()
        {
            return DIEntity.Instance.GetImpl<M>();
        }


        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //是否需要验证登录
            bool needLogin = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoLogin), true).Length == 1 ? false : true;
            if (needLogin)
            {
                if (LoginStatus==null)
                {//登录过期
                    filterContext.Result = new RedirectResult("/User/TimeOut");
                }
            }
        }


        /// <summary>
        /// 在action返回结果之后的处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (LoginStatus!=null)
            {
                var request = filterContext.HttpContext.Request;
                if (request.HttpMethod.ToUpper() == MethodType.GET && filterContext.Result.ToString().Contains("View"))
                {
                    if (Session[SessionType.MENU]!= null)
                    {                     //获取当前请求的菜单
                        var CurrentMenu = ((IEnumerable<dynamic>)Session[SessionType.MENU]).FirstOrDefault(m => m.sUrl == request.Url.LocalPath);
                        if (CurrentMenu != null && Session[SessionType.BTN] != null)
                        {
                            var btnList = ((IEnumerable<dynamic>)Session[SessionType.BTN]).Where(m => m.sParentId == CurrentMenu.ID.ToString());
                            ViewBag.@Btn = btnList;
                        }
                    }
                }
            }
        }
    }
}