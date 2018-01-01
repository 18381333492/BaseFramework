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
using Framework.Utility.Tools;

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
            if (Request.HttpMethod.ToUpper() ==MethodType.GET)
            {
                return View();
            }
            else
            {
                return Content(Manager.GetList(pageInfo));
            }
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleList()
        {
            var data = Manager.GetRoleList();
            return Content(data);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public ActionResult Add(ES_Role Role)
        {
            if (Request.HttpMethod.ToUpper() ==MethodType.GET)
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
            if (Request.HttpMethod.ToUpper() ==MethodType.GET)
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
        public ActionResult PowerSet(Guid ID)
        {
            if (Request.HttpMethod.ToUpper() ==MethodType.GET)
            {
                ViewBag.ID = ID;
                return View();
            }
            else
            {
                var role = Manager.Get(ID);
                string sPowerStr = role.sPowerIds;
                List<string> sMenuIds=new List<string>();
                List<string> sBtnIds=new List<string>();
                if (sPowerStr != null && sPowerStr.Contains("|"))
                {
                    sMenuIds = sPowerStr.Split('|')[0].Split(',').ToList();
                    sBtnIds = sPowerStr.Split('|')[1].Split(',').ToList();
                }
                var ListTree = new List<ItemTree>();
                //组织菜单数据
                foreach(var item in (IEnumerable<dynamic>)Session[SessionType.MENU])
                {
                    var tree = new ItemTree(){
                        id = item.ID.ToString(),
                        pid = item.sParentId,
                        text = item.sName,
                        iconCls = item.sIcon,
                        attributes = item.iType, //1表示菜单 2 表示按钮
                    };
                    if (sMenuIds.Contains(item.ID.ToString()))
                        tree.selected = true;
                    ListTree.Add(tree);
                }
                //组织按钮数据
                foreach (var item in (IEnumerable<dynamic>)Session[SessionType.BTN])
                {
                    var tree = new ItemTree()
                    {
                        id = item.ID.ToString(),
                        pid = item.sParentId,
                        text = item.sName,
                        iconCls = item.sIcon,
                        attributes = item.iType, //1表示菜单 2 表示按钮
                    };
                    if (sBtnIds.Contains(item.ID.ToString()))
                        tree.selected = true;
                    ListTree.Add(tree);
                }
                var treeString = JsonHelper.GetTreeData(ListTree);
               return Content(treeString);
            }
        }

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public ActionResult PowerSave(ES_Role Role)
        {
            if (Manager.PowerSave(Role) > 0)
                result.success = true;
            return Json(result);
        }
    }
}
