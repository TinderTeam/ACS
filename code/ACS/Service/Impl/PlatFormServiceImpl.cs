using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po;
using ACS.Dao;
using ACS.Test;
namespace ACS.Service.Impl
{
    public class PlatFormServiceImpl : PlatFormService
    {
       

        /// <summary>
        /// 根据用户ID获取 主菜单
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
       public  MenuTreeModel getMenuTreeByUserID(int userid)
        {
            MenuTreeModel tree = new MenuTreeModel();

            tree.MenuTreeItemList = Stub.getTreeItemList();

            return tree;
        }

    }
}