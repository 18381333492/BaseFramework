using Framework.Entity;
using Framework.Interfaces;
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
    /// 按钮控制器
    /// </summary>
    public class ButtonController : BaseController<IButton>
    {
        //
        // GET: /Button/

        /// <summary>
        /// 按钮列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PageInfo pageInfo)
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                return View();
            }
            else
            {
                return Content(Manager.GetList(pageInfo));
            }
        }

        /// <summary>
        /// 获取子菜单Combobox的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChildComboboxMenuList()
        {
            var manager = GetImpl<IMenu>();
            return Content(manager.GetChildComboboxList());
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public ActionResult Add(ES_Button Button)
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                return View();
            }
            else
            {
                if (Manager.Insert(Button) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public ActionResult Edit(ES_Button Button)
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                return View(Manager.Get(Button.ID));
            }
            else
            {
                if (Manager.Update(Button) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="ButtonIds"></param>
        /// <returns></returns>
        public ActionResult Cancel(string ButtonIds)
        {
            if (Manager.Cancel(ButtonIds) > 0)
                result.success = true;
            return Json(result);
        }
    }
}
