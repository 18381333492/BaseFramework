using Framework.Entity;
using Framework.Interfaces;
using Framework.Utility.Enums;
using Framework.Utility.Models;
using Framework.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.Web.Controllers
{
    /// <summary>
    /// 微信公众号控制器
    /// </summary>
    public class WeChatController : BaseController<IWeChat>
    {
        //
        // GET: /WeChat/

        /// <summary>
        /// 获取微信列表数据视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PageInfo pageInfo, int? iType)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                return Content(Manager.GetList(pageInfo, iType));
            }
        }

        /// <summary>
        /// 添加微信公众号
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public ActionResult Add(ES_WeChat WeChat)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                if (Manager.Insert(WeChat) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 编辑微信公众号
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public ActionResult Edit(ES_WeChat WeChat)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View(Manager.Get(WeChat.ID));
            }
            else
            {
                if (Manager.Update(WeChat) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 删除微信公众号
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public ActionResult Cancel(string WeChatIds)
        {
            if (Manager.Cancel(WeChatIds) > 0)
                result.success = true;
            return Json(result);
        }

        /// <summary>
        /// 成为开发者
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Developer(Guid ID)
        {
            if (Manager.Developer(ID) > 0)
                result.success = true;
            return Json(result);
        }
    }
}
