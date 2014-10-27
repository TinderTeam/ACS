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
using ACS.Test;
namespace ACS.Service.Impl
{
    public class PlatFormServiceImpl : PlatFormService
    {
        SysMenuDao sysMenuDao = DaoContext.getInstance().getSysMenuDao();
        PrivilegeDao privilegeDao = DaoContext.getInstance().getPrivilegeDao();
        /// <summary>
        /// 根据用户ID获取 主菜单
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
       public  MenuTreeModel getMenuTreeByUserID(int userid)
        {
           MenuTreeModel tree;
           //通过用户权限 获取应用列表
           List<Privilege> privilegeList = privilegeDao.getListByMaster(userid,ServiceConstant.SYS_MASTER_TYPE_USER);
           
           //过去菜单权限
           List<Sys_Menu> menuList = sysMenuDao.getListByPrivilegeList(privilegeList);
           tree=ModelConventService.toMenuTreeModel(menuList);
           return tree;
        }

    }
}