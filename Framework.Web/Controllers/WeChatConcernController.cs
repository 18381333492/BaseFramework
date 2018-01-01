using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Utility.Enums;
using Framework.Web.App_Start;
using Framework.Interfaces;

namespace Framework.Web.Controllers
{
    /// <summary>
    /// 微信公众号关注回复控制器
    /// </summary>
    public class WeChatConcernController : BaseController<IWeChatConcern>
    {
        //
        // GET: /WeChatConcern/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 保存关注回复的操作
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(string sWeChatId)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                return Json(null);
            }
        }

    }
}
