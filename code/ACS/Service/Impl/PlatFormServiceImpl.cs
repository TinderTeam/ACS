using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po;
using ACS.Models.Po.Sys;
using ACS.Models.Po.CF;
using ACS.Dao;
using ACS.Common.Dao;
using ACS.Test;
using ACS.Common.Dao.datasource;
using ACS.Common.Constant;
using ACS.Service.Constant;
namespace ACS.Service.Impl
{
    public class PlatFormServiceImpl : PlatFormService
    {
        CommonDao<Sys_Menu> sysMenuDao = DaoContext.getInstance().getSysMenuDao();
        CommonDao<Privilege> privilegeDao = DaoContext.getInstance().getPrivilegeDao();
        /// <summary>
        /// 根据用户ID获取 主菜单
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
       public  TreeModel getMenuTreeByUserID(int userid)
        {          
            List<Sys_Menu> menuList=PrivilegeCache.getSysMenuListByID(userid);
            TreeModel tree = ModelConventService.toMenuTreeModel(menuList);            
            return tree;
        }


       public AbstractDataSource<Privilege> getPrivilegeList(Privilege filter)
       {
           List<QueryCondition> conditionList = new List<QueryCondition>();
           AbstractDataSource<Privilege> dataSource = new DatabaseSourceImpl<Privilege>(conditionList);
           return dataSource;
       }
    }
}