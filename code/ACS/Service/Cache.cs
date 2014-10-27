using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po;
using ACS.Models.Po.Sys;
using ACS.Models.Po.CF;
using ACS.Models.Po.View;
using ACS.Service;
using ACS.Models.Model;
using ACS.Dao;
using ACS.Test;
namespace ACS.Service
{
    public class PrivilegeCache
    {

        SysMenuDao sysMenuDao = DaoContext.getInstance().getSysMenuDao();
        PrivilegeDao privilegeDao = DaoContext.getInstance().getPrivilegeDao();
        UserRoleViewDao userRoleViewDao = DaoContext.getInstance().getUserRoleViewDao();
        private static Dictionary<int,List<UserRoleView>> userRoleMap=null;
        private static Dictionary<int, PrivilegeSet> userPrivilegeMap = null;
        private static Dictionary<int, List<Sys_Menu>> userMenuMap = null;
        
        public static List<UserRoleView> getUserRoleListByID(int userid){
           
            if (userRoleMap == null)
            {
                initCache();
                if(!userRoleMap.ContainsKey(userid)){
                    addCache(userid);
                }
            }
            List<UserRoleView> list = userRoleMap[userid];
            return list;
        }

        public static List<Privilege> getUserPrivilegeListByID(int userid)
        {
            if (userPrivilegeMap == null)
            {
                initCache();
                if (!userPrivilegeMap.ContainsKey(userid))
                {
                    addCache(userid);
                }
            }
            List<Privilege> list = userPrivilegeMap[userid];
            return list;
        }

        public static List<Sys_Menu> getSysMenuListByID(int userid)
        {
            if (userMenuMap == null)
            {
                initCache();
                if (!userMenuMap.ContainsKey(userid))
                {
                    addCache(userid);
                }
            }
            List<Sys_Menu> list = userMenuMap[userid];
            return list;
        }

       
        //增加一个缓存
        private static void addCache(int userid)
        {
            //用户角色表加入缓存
            List<UserRoleView> userRoleList = userRoleViewDao.getListByUser(userid);
            userRoleMap.Add(userid,userRoleList);


            //根据用户加入权限
            List<Privilege> userPrivilegeList = privilegeDao.getListByMaster(
                userid,
                ServiceConstant.SYS_MASTER_TYPE_USER
                );
            PrivilegeSet set = new PrivilegeSet();

            set.AddList(userPrivilegeList);
            foreach (UserRoleView userRole in userRoleList)
            {
                List<Privilege> rolePrivilegeList = privilegeDao.getListByMaster(
                userRole.RoleID,
                ServiceConstant.SYS_MASTER_TYPE_ROLE
                );
                set.AddList(rolePrivilegeList);
            }
            userPrivilegeMap.Add(userid,set);


            //获取应用权限列表
            List<Privilege> list = set.Values.ToList<Privilege>();
            
            //通过应用权限列表获取菜单列表
            List<int> appIDList =  getAppPrivilegeList(list);


            //加入用户菜单表
            List<Sys_Menu> menuList = sysMenuDao.getListByAppIDList(
                appIDList
                );
            userMenuMap.Add(userid, menuList);
        }

        //初始化用户权限
        private static void initCache()
        {
            //用户角色表加入缓存
            userRoleMap=new Dictionary<int,List<UserRoleView>>();
            //用户
            userPrivilegeMap = new Dictionary<int, PrivilegeSet>();

            userMenuMap = new Dictionary<int, List<Sys_Menu>>();
        }
        //获取应用权限编号
        private List<int> getAppPrivilegeList(List<Privilege> list)
        {
            List<int> intList = new List<int>();
            foreach(Privilege p in list ){
                if (
                    p.PrivilegeMaster.Equals(ServiceConstant.SYS_MASTER_TYPE_APP)
                    && 
                    p.PrivilegeMasterValue.Equals(ServiceConstant.SYS_MASTER_VALUE_ALLOW)
                    )
                {
                    intList.Add(p.PrivilegeID);
                }
            }
            return intList; 
        }
    }
}