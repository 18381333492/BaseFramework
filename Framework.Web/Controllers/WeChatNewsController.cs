using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.Web.Controllers
{
    /// <summary>
    /// 图文素材列表
    /// </summary>
    public class WeChatNewsController : Controller
    {
        //
        // GET: /WeChatNews/

        public ActionResult Index()
        {
            return View();
        }

    }
}
