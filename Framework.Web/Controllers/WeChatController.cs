using Framework.Entity;
using Framework.Interfaces;
using Framework.Utility.Enums;
using Framework.Utility.Models;
using Framework.Web.App_Start;
using Framework.WeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Utility.Tools;

namespace Framework.Web.Controllers
{
    /// <summary>
    /// 微信公众号控制器
    /// </summary>
    public class WeChatController : BaseController<IWeChat>
    {
        //
        // GET: /WeChat/

        /// <summary>
        /// 获取微信列表数据视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PageInfo pageInfo, int? iType)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                return Content(Manager.GetList(pageInfo, iType));
            }
        }

        /// <summary>
        /// 获取微信公众号列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            var data = Manager.GetList();
            return Content(data);
        }

        /// <summary>
        /// 添加微信公众号
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public ActionResult Add(ES_WeChat WeChat)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                if (Manager.Insert(WeChat) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 编辑微信公众号
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public ActionResult Edit(ES_WeChat WeChat)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View(Manager.Get(WeChat.ID));
            }
            else
            {
                if (Manager.Update(WeChat) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 删除微信公众号
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public ActionResult Cancel(string WeChatIds)
        {
            if (Manager.Cancel(WeChatIds) > 0)
                result.success = true;
            return Json(result);
        }

        /// <summary>
        /// 成为开发者
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Developer(Guid ID)
        {
            if (Manager.Developer(ID) > 0)
                result.success = true;
            return Json(result);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateMenu()
        {
            var weChat = Manager.GetDeveloper();//获取开发者微信公众号
            //获取公众号的所有菜单
            var menuList = GetImpl<IWeChatMenu>().GetMenuByWeChat(weChat.ID.ToString());
            //二级菜单
            var childList = menuList.Where(m => string.IsNullOrEmpty(m.sParentMenuId) == false).ToList();
            var body = new List<dynamic>();
            foreach(var item in menuList)
            {
                if (string.IsNullOrEmpty(item.sParentMenuId))
                {//一级菜单
                    dynamic obj = new System.Dynamic.ExpandoObject();
                    obj.name = item.sMenuName;
                    obj.type = item.sTiggerType;
                    obj.key = item.sKey;
                    obj.url = item.sUrl;
                    var obj_sub = new List<dynamic>();
                    foreach(var child in childList)
                    {
                        if (item.ID.ToString() == child.sParentMenuId)
                        {
                            obj_sub.Add(new
                            {
                                name = child.sMenuName,
                                type = child.sTiggerType,
                                key = child.sKey,
                                url = child.sUrl
                            });
                        }
                    }
                    obj.sub_button = obj_sub;
                    body.Add(obj);
                }
            }
            string bodyStr = JsonHelper.ToJsonString(new { button= body });
            IWeChatRequest request = new CreateAutoMenuRequest();
            CreateAutoMenuModel model = new CreateAutoMenuModel();
            model.sAppId = weChat.sAppId;
            model.sAppSecret = weChat.sAppSecret;
            model.sBody = bodyStr;
            CreateAutoMenuRespone respone =request.Execute<CreateAutoMenuRespone>(model);
            if (respone.errcode == 0)
            {
                result.success = true;
                result.info = "菜单创建成功";
            }
            else
                result.info = respone.errcode.ToString() + respone.errmsg;
            return Json(result);
        }
    }
}
