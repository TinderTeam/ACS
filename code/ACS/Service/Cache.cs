using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po;
using ACS.Models.Po.Sys;
using ACS.Models.Po.CF;
using ACS.Common.Constant;
using ACS.Service;
using ACS.Models.Model;
using ACS.Dao;
using ACS.Test;
using ACS.Common.Dao;
namespace ACS.Service
{
    public class PrivilegeCache
    {


        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Dictionary<int,List<UserRole>> userRoleMap=null;
        private static Dictionary<int, PrivilegeSet> userPrivilegeMap = null;
        private static Dictionary<int, List<Sys_Menu>> userMenuMap = null;
        
        public static List<UserRole> getUserRoleListByID(int userid){
           
            if (userRoleMap == null)
            {
                initCache();
                if(!userRoleMap.ContainsKey(userid)){
                    addCache(userid);
                }
            }
            List<UserRole> list = userRoleMap[userid];
            return list;
        }


        public static PrivilegeSet getUserPrivilegeListByID(int userid)
        {
            if (userPrivilegeMap == null)
            {
                initCache();
                if (!userPrivilegeMap.ContainsKey(userid))
                {
                    addCache(userid);
                }
            }
            PrivilegeSet set = userPrivilegeMap[userid];
            return set;
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
            log.Info("add cache where userID = " + userid);

            CommonDao<Sys_Menu> sysMenuDao = DaoContext.getInstance().getSysMenuDao();
            CommonDao<Privilege> privilegeDao = DaoContext.getInstance().getPrivilegeDao();
            CommonDao<UserRole> userRoleDao = DaoContext.getInstance().getUserRoleDao();

            //用户角色表加入缓存
            QueryCondition condition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                UserRole.USER_ID, 
                userid.ToString()
                );
            List<UserRole> userRoleList = userRoleDao.getAll(condition);
            userRoleMap.Add(userid,userRoleList);
            log.Debug("UserRoleView of userID = " + userRoleList);

            //根据用户加入权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add( new QueryCondition(
                ConditionTypeEnum.EQUAL,
                Privilege.MASTER_VALUE,
                userid.ToString()
                )
            );
            conditionList.Add(new QueryCondition(
                  ConditionTypeEnum.EQUAL,
                  Privilege.MASTER,
                  ServiceConstant.SYS_MASTER_TYPE_USER
                  )
            );
          
            List<Privilege> userPrivilegeList = privilegeDao.getAll(conditionList);
            log.Debug("userPrivilegeList of userID = " + userPrivilegeList);

            PrivilegeSet set = new PrivilegeSet();
            set.AddList(userPrivilegeList);
            foreach (UserRole userRole in userRoleList)
            {
                
                ///设置条件：masterValue=RoleID;masterType="ROLE"
                List<QueryCondition> privilegeConditionList = new List<QueryCondition>();
                privilegeConditionList.Add(new QueryCondition(
                    ConditionTypeEnum.EQUAL,
                    Privilege.MASTER_VALUE,
                    userRole.RoleID.ToString()
                    )
                );
                privilegeConditionList.Add(new QueryCondition(
                      ConditionTypeEnum.EQUAL,
                      Privilege.MASTER,
                      ServiceConstant.SYS_MASTER_TYPE_ROLE
                      )
                );

                List<Privilege> rolePrivilegeList = privilegeDao.getAll(privilegeConditionList);
                set.AddList(rolePrivilegeList);
            }
            userPrivilegeMap.Add(userid,set);

            //获取应用权限列表
            List<Privilege> list = set.Values.ToList<Privilege>(); 
        
            //通过应用权限列表获取菜单列表
            List<String> appIDList =  getAppPrivilegeList(list);
            log.Debug("appIDList of userID = " + appIDList);

            ///设置条件：在idList中
            QueryCondition inCondition = new QueryCondition(
                  ConditionTypeEnum.IN,
                  Sys_Menu.APP_ID,
                  appIDList
                  );

            //加入用户菜单表
            List<Sys_Menu> menuList = sysMenuDao.getAll(inCondition);
            
            //加入父类菜单
            List<String> fatheridList = new List<String>();
            foreach (Sys_Menu menu in menuList)
            {
               String str= menu.MenuParentNo;
               fatheridList.Add(str);
            }
            
            QueryCondition fatherinCondition = new QueryCondition(
                  ConditionTypeEnum.IN,
                  Sys_Menu.MEMU_ID,
                  fatheridList
                  );
            List<Sys_Menu> fatherList = sysMenuDao.getAll(fatherinCondition);
            foreach (Sys_Menu m in fatherList)
            {
                menuList.Add(m);
            }
            log.Debug("menuList of userID = " + menuList);

            userMenuMap.Add(userid, menuList);
        }

        //初始化用户权限
        private static void initCache()
        {
            log.Info("init PrivilegeCache...");
            //用户角色表加入缓存
            userRoleMap=new Dictionary<int,List<UserRole>>();
            //用户
            userPrivilegeMap = new Dictionary<int, PrivilegeSet>();

            userMenuMap = new Dictionary<int, List<Sys_Menu>>();
        }
        //获取应用权限编号
        private static List<String> getAppPrivilegeList(List<Privilege> list)
        {
            List<String> id = new List<String>();
            foreach(Privilege p in list ){
                if (
                    p.PrivilegeAccess.Equals(ServiceConstant.SYS_MASTER_TYPE_APP)
                    && 
                    p.PrivilegeOperation.Equals(ServiceConstant.SYS_MASTER_VALUE_ALLOW)
                    )
                {
                    id.Add(p.PrivilegeAccessValue.ToString());
                }
            }
            return id; 
        }
    }
}