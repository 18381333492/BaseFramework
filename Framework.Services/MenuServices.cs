using Framework.DBAccess.EF;
using Framework.Entity;
using Framework.Interfaces;
using Framework.Utility.Models;
using Framework.Utility.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Services
{
    public class MenuServices: IMenu
    {

        /// <summary>
        /// 获取所有的菜单数据
        /// </summary>
        /// <returns></returns>
        public override string GetList()
        {
            var allMenuList = query.QueryList<ES_Menu>("select * from ES_Menu where bIsDeleted=0 order by iorder");
            //组装数据
            var result = JsonHelper.GetGridTreeData(allMenuList.ToList<dynamic>());

            return result;
        }

        /// <summary>
        /// 获取菜单ComboTree的数据
        /// </summary>
        /// <returns></returns>
        public override string GetComboTreeList()
        {
            var comboTreeMenuList = query.QueryList<ES_Menu>("select * from ES_Menu where bIsDeleted=0 order by iorder");
            //组装数据
            var result = JsonHelper.GetTreeData(comboTreeMenuList.Select(m =>
            {
                return new ItemTree()
                {
                    id = m.ID.ToString(),
                    pid = m.sParentMenuId,
                    text = m.sMenuName,
                    attributes = m.sMenuUrl
                };
            }).ToList(),true);

            return result;
        }
    
        /// <summary>
        /// 获取用户的菜单列表
        /// </summary>
        /// <returns></returns>
        public override string GetUserMenuList()
        {
            var userMenuList = query.QueryList<ES_Menu>("select * from ES_Menu where bIsDeleted=0 order by iorder");
            //组装数据
            var result = JsonHelper.GetTreeData(userMenuList.Select(m =>
            {
                return new ItemTree()
                {
                    id = m.ID.ToString(),
                    pid = m.sParentMenuId,
                    text = m.sMenuName,
                    attributes = m.sMenuUrl
                };
            }).ToList());

            return result;
        }

        /// <summary>
        /// 获取所有的子菜单ComboTree数据
        /// </summary>
        /// <returns></returns>
        public override string GetChildComboboxList()
        {
            var childComboTreeMenuList = query.QueryList<ES_Menu>(@"select ID,sMenuName from ES_Menu 
                                                                           where bIsDeleted=0 and sParentMenuId is not null order by iorder");
            //组装数据
            var result = JsonHelper.ToJsonString(childComboTreeMenuList);
            return result;
        }

        /// <summary>
        /// 根据用户获取相应的菜单和按钮
        /// </summary>
        /// <returns></returns>
        public override string GetMenuAndBtnByUser()
        {
            var menuList= query.QueryList<ES_Menu>("select * from ES_Menu where bIsDeleted=0 order by iorder");
            var btnList= query.QueryList<ES_Button>("select * from ES_Button where bIsDeleted=0 order by iorder");
            var ItemTreeList = new List<ItemTree>();
            ItemTreeList = menuList.Select(m =>
            {
                  return new ItemTree()
                  {
                      id = m.ID.ToString(),
                      pid = m.sParentMenuId,
                      text = m.sMenuName,
                      state = "closed"
                  };
            }).ToList();
            ItemTreeList=ItemTreeList.Concat(btnList.Select(m =>
            {
                return new ItemTree()
                {
                    id = m.ID.ToString(),
                    pid = m.sMenuId,
                    text = m.sButtonName,
                    iconCls = m.sButtonIcon,
                    state = "closed"       
                };
            }).ToList()).ToList();
            //组装数据
            var result =JsonHelper.GetTreeData(ItemTreeList);
            return result;
        }


        /// <summary>
        /// 根据主键获取菜单对象
        /// </summary>
        /// <param name="MenuId"></param>
        /// <returns></returns>
        public override ES_Menu Get(Guid MenuId)
        {
            var obj = query.Find<ES_Menu>(MenuId);
            return obj;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="Menu"></param>
        /// <returns></returns>
        public override int Insert(ES_Menu Menu)
        {
            using (var Context=new Entities())
            {
                Menu.ID = GuidHelper.NewGuid();
                Menu.dInsertTime = DateTime.Now;
                Menu.bIsDeleted = false;
                Context.Insert<ES_Menu>(Menu);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="Menu"></param>
        /// <returns></returns>
        public override int Update(ES_Menu Menu)
        {
            using (var Context = new Entities())
            {
                Context.Update<ES_Menu>(Menu);
                return Context.SaveExtendChanges();
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="MenuIds"></param>
        /// <returns></returns>
        public override int Cancel(string MenuIds)
        {
            using (var Context = new Entities())
            {
                return Context.Cancel<ES_Menu>(MenuIds);
            }
        }
    }
}
