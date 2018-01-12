using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Utility.Enums;
using Framework.Web.App_Start;
using Framework.Interfaces;
using Framework.Utility.Models;
using Framework.Entity;

namespace Framework.Web.Controllers
{
    /// <summary>
    /// 微信关键回复
    /// </summary>
    public class WeChatKeyWordController : BaseController<IWeChatKeyWord>
    {
        //
        // GET: /WeChatKeyWord/

        /// <summary>
        /// 微信关键字视图列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PageInfo pageInfo,string sWeChatId)
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
        /// 添加关键字
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public ActionResult Add(ES_WeChatKeyWord KeyWord)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                if (Manager.Insert(KeyWord) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 编辑关键字
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public ActionResult Edit(ES_WeChatKeyWord KeyWord)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View(Manager.Get(KeyWord.ID));
            }
            else
            {
                if (Manager.Update(KeyWord) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 删除关键字
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel(string Ids)
        {
            if (Manager.Cancel(Ids) > 0)
                result.success = true;
            return Json(result);
        }


    }
}
