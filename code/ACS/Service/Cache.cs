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
            CommonDao<Sys_Menu> sysMenuDao = DaoContext.getInstance().getSysMenuDao();
            CommonDao<Privilege> privilegeDao = DaoContext.getInstance().getPrivilegeDao();
            ViewDao<UserRoleView> userRoleViewDao = DaoContext.getInstance().getUserRoleViewDao();

            //用户角色表加入缓存
            QueryCondition condition = new QueryCondition(
                ConditionTypeEnum.EQUAL,
                UserRoleView.USER_ID, 
                userid.ToString()
                );
            List<UserRoleView> userRoleList = userRoleViewDao.getAll();
            userRoleMap.Add(userid,userRoleList);

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
                  userid.ToString()
                  )
            );
          
            List<Privilege> userPrivilegeList = privilegeDao.getAll(conditionList);

            PrivilegeSet set = new PrivilegeSet();
            set.AddList(userPrivilegeList);
            foreach (UserRoleView userRole in userRoleList)
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


            ///设置条件：在idList中
            QueryCondition inCondition = new QueryCondition(
                  ConditionTypeEnum.IN,
                  Sys_Menu.APP_ID,
                  appIDList
                  );

            //加入用户菜单表
            List<Sys_Menu> menuList = sysMenuDao.getAll(inCondition);
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
        private static List<String> getAppPrivilegeList(List<Privilege> list)
        {
            List<String> id = new List<String>();
            foreach(Privilege p in list ){
                if (
                    p.PrivilegeMaster.Equals(ServiceConstant.SYS_MASTER_TYPE_APP)
                    && 
                    p.PrivilegeMasterValue.Equals(ServiceConstant.SYS_MASTER_VALUE_ALLOW)
                    )
                {
                    id.Add(p.PrivilegeID.ToString());
                }
            }
            return id; 
        }
    }
}