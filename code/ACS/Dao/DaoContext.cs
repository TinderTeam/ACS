using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Dao;
using ACS.Dao.impl;
namespace ACS.Dao
{
    public class DaoContext
    {
        static DaoContext daoContext;
        private PrivilegeDao privilegeDao;
        private SysMenuDao sysMenuDao;
        private UserDao userDao;
        private EmployeeDao employeeDao;
        public static DaoContext getInstance()
        {
            if (daoContext == null)
            {
                daoContext = new DaoContext();
            }
            return daoContext;
        }


        public SysMenuDao getSysMenuDao()
        {
            if (sysMenuDao == null)
            {
                sysMenuDao = new SysMenuDaoImpl();
            }
            return sysMenuDao;
        }

        public PrivilegeDao getPrivilegeDao()
         {
             if (privilegeDao == null)
             {
                 privilegeDao = new PrivilegeDaoImpl();
             }
             return privilegeDao;
         }

         public UserDao getUserDao()
         {
             if (userDao == null)
             {
                 userDao = new UserDaoImpl();
             }
             return userDao;
         }

         public EmployeeDao getEmployeeDao()
         {
             if (employeeDao == null)
             {
                 employeeDao = new EmployeeDaoImpl();
             }
             return employeeDao;
         }

        

    }
}