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

namespace Framework.Web.Controllers
{
    /// <summary>
    /// 数据字典控制器
    /// </summary>
    public class DictionaryController : BaseController<IDictionary>
    {
        //
        // GET: /Dictionary/

        /// <summary>
        /// 数据字典列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PageInfo pageInfo)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                return Content(Manager.GetList(pageInfo));
            }
        }

        /// <summary>
        /// 根据字典类型获取字典数据
        /// </summary>
        /// <param name="sValueType"></param>
        /// <returns></returns>
        public ActionResult GetComboBoxData(string sValueType,bool bSelect)
        {
            return Content(Manager.GetComboBoxData(sValueType, bSelect));
        }

        /// <summary>
        /// 添加数据字典
        /// </summary>
        /// <param name="Dictionary"></param>
        /// <returns></returns>
        public ActionResult Add(ES_Dictionary Dictionary)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                if (Manager.Insert(Dictionary) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 编辑数据字典
        /// </summary>
        /// <param name="Dictionary"></param>
        /// <returns></returns>
        public ActionResult Edit(ES_Dictionary Dictionary)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View(Manager.Get(Dictionary.ID));
            }
            else
            {
                if (Manager.Update(Dictionary) > 0)
                    result.success = true;
                return Json(result);
            }
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="DictionaryIds"></param>
        /// <returns></returns>
        public ActionResult Cancel(string DictionaryIds)
        {
            if (Manager.Cancel(DictionaryIds) > 0)
                result.success = true;
            return Json(result);
        }

    }
}
