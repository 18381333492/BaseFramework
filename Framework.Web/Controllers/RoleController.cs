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
    /// 角色控制器
    /// </summary>
    public class RoleController : BaseController<IRole>
    {
        //
        // GET: /Role/

        /// <summary>
        /// 角色视图列表
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
        /// 添加角色
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public ActionResult Add(ES_Role Role)
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                return View();
            }
            else
            {
                if (Manager.Insert(Role) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public ActionResult Edit(ES_Role Role)
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                return View(Manager.Get(Role.ID));
            }
            else
            {
                if (Manager.Update(Role) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleIds"></param>
        /// <returns></returns>
        public ActionResult Cancel(string RoleIds)
        {
            if (Manager.Cancel(RoleIds) > 0)
                result.success = true;
            return Json(result);
        }

        /// <summary>
        /// 权限分配
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public ActionResult PowerSet(ES_Role Role)
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                return View();
            }
            else
            {
                var manage = GetImpl<IMenu>();
                return Content(manage.GetMenuAndBtnByUser());
            }
        }
    }
}
