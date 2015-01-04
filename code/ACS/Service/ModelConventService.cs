using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Models.Po.CF;
using ACS.Models.Po.Sys;

using ACS.Common.Util;
using ACS.Common.Dao;
using ACS.Common.Constant;
namespace ACS.Service
{
    public class ModelConventService
    {
        public static List<int> toIDList(String str)
        {
            List<int> list = new List<int>();
            foreach(String  i in str.Split(','))
            {
                list.Add(Convert.ToInt16(i));
            }
            return list;

        }

        public static List<string> toIDStrList(String str)
        {
            List<string> list = new List<string>();
            foreach (String i in str.Split(','))
            {
                list.Add(i);
            }
            return list;

        }
 
        // <summary>
        // 转化为menuTreemodel
        // </summary>
        public static List<TreeModel> toMenuTreeModelList(List<Sys_Menu> sysMenuList)
        {
            //sysMenu转换为menuTree
            List<TreeModel> menuTreeList = new List<TreeModel>();
            foreach (Sys_Menu sysMenu in sysMenuList)
            {
                TreeModel menuTree = new TreeModel();
                menuTree.Id = sysMenu.MenuID.ToString();
                menuTree.Pid = sysMenu.MenuParentNo;
                menuTree.MenuName = sysMenu.MenuName;
                menuTree.Url = sysMenu.MenuURL;
                menuTreeList.Add(menuTree);
            }
            return menuTreeList;
        }

        //用户管理，设备域管理调用
        public static List<TreeModel> toDeviceTreeModel(List<Control> controlList)
        {
            //Control转换为menuTree
            List<TreeModel> menuTreeList = new List<TreeModel>();
            foreach (Control control in controlList)
            {
                TreeModel menuTree = new TreeModel();
                menuTree.Id = control.ControlID.ToString();
                menuTree.MenuName = control.ControlName;
                menuTreeList.Add(menuTree);
            }
            return menuTreeList;
        }

        internal static List<QueryCondition> getAccessDetailIDConditionByViewList(List<AccessDetailView> viewList)
        {
            List<QueryCondition> list = new List<QueryCondition>();
            foreach (AccessDetailView view in viewList)
            {
                QueryCondition condition = new QueryCondition(
                       ConditionTypeEnum.EQUAL,
                           AccessDetail.ACCESS_DETAIL_ID,
                           view.AccessDetailID.ToString()
                   );
                list.Add(condition);
            }
            return list;
        }

        /// <summary>
        /// 根据门获得门序号
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        internal static byte toDoorIndex(Door door)
        {
            return 1;
        }

        


    }
}