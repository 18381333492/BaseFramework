using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Utility.Enums;
using Framework.Utility.Models;
using Framework.Web.App_Start;
using Framework.Interfaces;
using Framework.Entity;

namespace Framework.Web.Controllers
{
    /// <summary>
    /// 微信公众号菜单
    /// </summary>
    public class WeChatMenuController : BaseController<IWeChatMenu>
    {
        //
        // GET: /WeChatMenu/

        /// <summary>
        /// 微信自定义菜单视图列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public ActionResult Index(PageInfo pageInfo, string sWeChatId)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                return Content(Manager.GetList(pageInfo, sWeChatId));
            }
        }

        /// <summary>
        /// 根据微信公众号获取相应的一级菜单
        /// </summary>
        /// <param name="sWeChatId"></param>
        /// <returns></returns>
        public ActionResult GetFirstMenuByWeChat(string sWeChatId)
        {
            var data = Manager.GetFirstMenuByWeChat(sWeChatId);
            return Content(data);
        }

        /// <summary>
        /// 添加微信公众号菜单
        /// </summary>
        /// <param name="WeChatMenu"></param>
        /// <returns></returns>
        public ActionResult Add(ES_WeChatMenu WeChatMenu)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                if (Manager.Insert(WeChatMenu) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 编辑微信公众号菜单
        /// </summary>
        /// <param name="WeChatMenu"></param>
        /// <returns></returns>
        public ActionResult Edit(ES_WeChatMenu WeChatMenu)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View(Manager.Get(WeChatMenu.ID));
            }
            else
            {
                if (Manager.Update(WeChatMenu) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 编辑微信公众号菜单
        /// </summary>
        /// <param name="WeChatMenu"></param>
        /// <returns></returns>
        public ActionResult Cancel(string sWeChatMenuIds)
        {
            if (Manager.Cancel(sWeChatMenuIds) > 0)
                result.success = true;
            return Json(result);
        }
    }
}
