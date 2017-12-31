using Framework.Interfaces;
using Framework.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Utility.Enums;
using Framework.Utility.Tools;
using Framework.Utility.Models;

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
                return Content(GetUserMenuList());
            }
        }

       
        /// <summary>
        /// 获取用户的菜单数据
        /// </summary>
        /// <returns></returns>
        private string GetUserMenuList()
        {
            var menuList =(IEnumerable<dynamic>)Session[SessionType.MENU];
            if (menuList != null)
            {
                //组装tree结构数据
                var treeString = JsonHelper.GetTreeData(menuList.Select(m =>
                {
                    return new ItemTree()
                    {
                        id = m.ID.ToString(),
                        pid = m.sParentId,
                        text = m.sName,
                        attributes = m.sUrl
                    };
                }).ToList());
                return treeString;
            }
            else
                return string.Empty;
        }
    }
}
