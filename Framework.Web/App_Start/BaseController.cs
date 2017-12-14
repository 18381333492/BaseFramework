using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.DI;
using Framework.Utility.Models;

namespace Framework.Web.App_Start
{
    public class BaseController<T>:Controller
    {
        //接口
        protected  T Manager
        {
            get;
        }

        /// <summary>
        /// 返回的响应结果
        /// </summary>
        protected ResponeResult result
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数实例化接口
        /// </summary>
        public BaseController()
        {
            Manager = GetImpl<T>();
            result = new ResponeResult();
        }

        /// <summary>
        /// 映射实现接口
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <returns></returns>
        protected static M GetImpl<M>()
        {
            return DIEntity.Instance.GetImpl<M>();
        }


        /// <summary>
        /// 在action返回结果之后的处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}