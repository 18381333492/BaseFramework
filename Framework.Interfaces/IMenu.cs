using Framework.DBAccess;
using Framework.DI;
using Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    /// <summary>
    /// 菜单接口
    /// </summary>
    public abstract class IMenu: IBase
    {

        /// <summary>
        /// 获取所有的菜单数据
        /// </summary>
        /// <returns></returns>
        public abstract string GetList();

        /// <summary>
        /// 获取菜单ComboTree的数据
        /// </summary>
        /// <returns></returns>
        public abstract string GetComboTreeList();

        /// <summary>
        /// 获取用户的菜单列表
        /// </summary>
        /// <returns></returns>
        public abstract string GetUserMenuList();

        /// <summary>
        /// 获取所有的子菜单Combobox数据
        /// </summary>
        /// <returns></returns>
        public abstract string GetChildComboboxList();

        /// <summary>
        /// 根据用户获取相应的菜单和按钮
        /// </summary>
        /// <returns></returns>
        public abstract string GetMenuAndBtnByUser();

        /// <summary>
        /// 根据主键获取菜单对象
        /// </summary>
        /// <param name="MenuId"></param>
        /// <returns></returns>
        public abstract ES_Menu Get(Guid MenuId);


        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="Menu"></param>
        /// <returns></returns>
        public abstract int Insert(ES_Menu Menu);


        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="Menu"></param>
        /// <returns></returns>
        public abstract int Update(ES_Menu Menu);


        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="MenuIds"></param>
        /// <returns></returns>
        public abstract int Cancel(string MenuIds);
       

    }
}
