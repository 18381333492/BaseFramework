using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Framework.Utility.Models;
using Newtonsoft.Json.Converters;

namespace Framework.Utility.Tools
{
    public class JsonHelper
    {

        /// <summary>
        /// 将对象序列化成JSON字符串
        /// </summary>
        /// <param name="o">序列化的对象</param>
        /// <returns></returns>
        public static string ToJsonString(object o)
        {
            return JsonConvert.SerializeObject(o, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }

        /// <summary>
        /// 将JSON字符串反序列化成泛型T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sJsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string sJsonString)
        {
            var result = default(T);
            result=JsonConvert.DeserializeObject<T>(sJsonString);
            return result;
        }

        /// <summary>
        /// 获取easyui tree的结构数据
        /// </summary>
        /// <param name="treeList"></param>
        /// <param name="Default">是否添加默认数据</param>
        /// <returns></returns>
        public static string GetTreeData(List<ItemTree> treeList,bool Default=false)
        {
            if (treeList.Count > 0)
            {
                var mainList = treeList.Where(m => string.IsNullOrEmpty(m.pid) == true);//一级父菜单
                var childList = treeList.Where(m => string.IsNullOrEmpty(m.pid) == false);//子菜单

                JArray mainArray = new JArray();
                if (Default)
                {//添加默认数据
                    var defaultObj = new JObject();
                    defaultObj.Add(new JProperty("id", string.Empty));
                    defaultObj.Add(new JProperty("text", "一级菜单"));
                    defaultObj.Add(new JProperty("attributes", string.Empty));
                    mainArray.Add(defaultObj);
                }
                foreach (var o in mainList)
                {
                    JObject main = new JObject();
                    JArray childArray = new JArray();//二级树形数组
                                                     //调用递归
                    childArray = RecursionTreeData(o, childList.ToList());
                    main.Add(new JProperty("id", o.id.ToString()));
                    main.Add(new JProperty("text", o.text));
                    main.Add(new JProperty("attributes", o.attributes));
                    main.Add(new JProperty("iconCls", o.iconCls));
                    main.Add(new JProperty("state",o.state));
                    main.Add(new JProperty("checked", o.selected));
                    main.Add(new JProperty("children", childArray));
                    mainArray.Add(main);
                }
                return mainArray.ToString();
            }
            else
            {
                return "[]";
            }
        }

        /// <summary>
        /// 递归调用组织Tree数据
        /// </summary>
        /// <param name="data"></param>
        private static JArray RecursionTreeData(ItemTree childItem, List<ItemTree> childList)
        {
            JArray childArray = new JArray();
            JArray temp = new JArray();
            foreach (var o in childList)
            {
                JObject child = new JObject();
                if (childItem.id==o.pid)
                {
                    temp = RecursionTreeData(o, childList);
                    child.Add(new JProperty("id", o.id.ToString()));
                    child.Add(new JProperty("text", o.text));
                    child.Add(new JProperty("attributes", o.attributes));
                    child.Add(new JProperty("iconCls", o.iconCls));
                    child.Add(new JProperty("state", o.state));
                    child.Add(new JProperty("checked", o.selected));
                    child.Add(new JProperty("children", temp));
                    childArray.Add(child);
                }
            }
            return childArray;
        }


        /// <summary>
        /// 获取easy ui gridTree的结构数据
        /// </summary>
        /// <returns></returns>
        public static string GetGridTreeData(List<dynamic> gridTreeList)
        {
            if (gridTreeList.Count > 0)
            {
                var mainList = gridTreeList.Where(m => string.IsNullOrEmpty(m.sParentMenuId) == true);//一级父菜单
                var childList = gridTreeList.Where(m => string.IsNullOrEmpty(m.sParentMenuId) == false);//子菜单

                JArray mainArray = new JArray();
                foreach (var n in mainList)
                {
                    var item = JObject.Parse(JsonHelper.ToJsonString(n));
                    JArray childArray = new JArray();
                    //递归调用
                    childArray = RecursionGridTree(n, childList.ToList());
                    item.Add(new JProperty("children", childArray));
                    mainArray.Add(item);     
                }
                return mainArray.ToString();
            }
            else
            {
                return "[]";
            }
        }

        /// <summary>
        /// 递归调用组装GridTree数据
        /// </summary>
        /// <param name="childItem"></param>
        /// <param name="childList"></param>
        /// <returns></returns>
        private static JArray RecursionGridTree(dynamic childItem, List<dynamic> childList)
        {
            JArray childArray = new JArray();
            JArray tempArray = new JArray();
            foreach (var o in childList)
            {        
                var temp = JObject.Parse(JsonHelper.ToJsonString(o));
                if (childItem.ID.ToString() == o.sParentMenuId)
                {
                     tempArray = RecursionGridTree(o, childList);
                     temp.Add(new JProperty("children", tempArray));
                     childArray.Add(temp);
                }
            }
            return childArray;
        }
    }
}
