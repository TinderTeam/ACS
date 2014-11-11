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
        CommonDao< User > userDao = DaoContext.getInstance().getUserDao();

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
        //用户权限管理
        //获取用户菜单权限树
       public TreeModel getPrivilegeMenuTree(string userID)
       {
           //获取所有权限树列表
           List<Sys_Menu> menuList = sysMenuDao.getAll();
           TreeModel allTree = ModelConventService.toMenuTreeModel(menuList);
           //获取所选用户对应的权限
           List<QueryCondition> conditionList = new List<QueryCondition>();
           conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL,Privilege.MASTER_VALUE,userID));
           conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL,Privilege.ACCESS, ServiceConstant.SYS_ACCESS_TYPE_APP));
           List<Privilege> userPrivilegeList = privilegeDao.getAll(conditionList);
           //对用户存在的权限菜单打勾
           foreach (Privilege i in userPrivilegeList)
           {
               foreach (TreeItem j in allTree.MenuTreeItemList)
               {
                   if (i.PrivilegeAccessValue.Equals(j.Id))
                   {
                       j.CheckNode = true;
                   }
               }
           }
           return allTree;
       }

       public AbstractDataSource<Privilege> getPrivilegeList(Privilege filter)
       {
           List<QueryCondition> conditionList = new List<QueryCondition>();
           AbstractDataSource<Privilege> dataSource = new DatabaseSourceImpl<Privilege>(conditionList);
           return dataSource;
       }

       public TreeModel getUserTree()
       {
           List<User> userList = userDao.getAll();
           TreeModel tree = ModelConventService.toUserTreeModel(userList);
           return tree;
       }

    }
}