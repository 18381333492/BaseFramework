using Framework.Interfaces;
using Framework.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.Web.Controllers
{
    public class DefaultController : BaseController<IMenu>
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {
                return Content(Manager.GetUserMenuList());
            }
        }
    }
}
