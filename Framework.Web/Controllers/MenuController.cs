using Framework.Entity;
using Framework.Interfaces;
using Framework.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.Web.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    public class MenuController: BaseController<IMenu>
    {
        //
        // GET: /Menu/

        /// <summary>
        /// treegrid菜单数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.HttpMethod.ToUpper()=="GET")
            {
                return View();
            }
            else
            {
                return Content(Manager.GetList());
            }
        }

        /// <summary>
        /// 获取菜单ComboTree的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetComboTreeList()
        {
            return Content(Manager.GetComboTreeList());
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="Menu"></param>
        /// <returns></returns>
        public ActionResult Add(ES_Menu Menu)
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                return View();
            }
            else
            {
                if (Manager.Insert(Menu) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="Menu"></param>
        /// <returns></returns>
        public ActionResult Edit(ES_Menu Menu)
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                return View(Manager.Get(Menu.ID));
            }
            else
            {
                if (Manager.Update(Menu) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="MenuIds"></param>
        /// <returns></returns>
        public ActionResult Cancel(string MenuIds)
        {
            if (Manager.Cancel(MenuIds) > 0)
            {
                result.success = true;
                result.info = "删除成功";
            }
            return Json(result);
        }
    }
}
