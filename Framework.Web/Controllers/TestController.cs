using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Utility.Tools;

namespace Framework.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public void Index()
        {
            CacheHelper.SetCache("fdsf", "sdsf", "4564");
        }

    }
}
