using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Dao;
using ACS.Common.Dao.impl;
using ACS.Common.Dao;
using ACS.Models.Po.Sys;
using ACS.Models.Po;
using ACS.Models.Po.CF;
using ACS.Models.Po.Business;

namespace ACS.Dao
{
    public class DaoContext
    {
        static DaoContext daoContext;
        private CommonDao<Privilege> privilegeDao;
        private CommonDao<Sys_Menu> sysMenuDao;
        private CommonDao<User> userDao;
        private CommonDao<Employee> employeeDao;
        private ViewDao<UserRoleView> userRoleViewDao;



        public static DaoContext getInstance()
        {
            if (daoContext == null)
            {
                daoContext = new DaoContext();
            }
            return daoContext;
        }


        public CommonDao<Sys_Menu> getSysMenuDao()
        {
            if (sysMenuDao == null)
            {
                sysMenuDao = new CommonDaoImpl<Sys_Menu>();
            }
            return sysMenuDao;
        }


        public ViewDao<UserRoleView> getUserRoleViewDao()
        {
            if (userRoleViewDao == null)
            {
                userRoleViewDao = new ViewDaoCommonImpl<UserRoleView>();
            }
            return userRoleViewDao;
        }

        public CommonDao<Privilege> getPrivilegeDao()
         {
             if (privilegeDao == null)
             {
                 privilegeDao = new CommonDaoImpl<Privilege>();
             }
             return privilegeDao;
         }
         public CommonDao<User> getUserDao()
         {
             if (userDao == null)
             {
                 userDao = new CommonDaoImpl<User>();
             }
             return userDao;
         }

        public CommonDao<Employee> getEmployeeDao()
         {
             if (employeeDao == null)
             {
                 employeeDao = new CommonDaoImpl<Employee>();
             }
             return employeeDao;
         }

        

    }
}