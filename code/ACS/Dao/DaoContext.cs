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
        private DbTableDao dbTableDao;
        private UserDao userDao;

        public static DaoContext getInstance()
        {
            if (daoContext == null)
            {
                daoContext = new DaoContext();
            }
            return daoContext;
        }
        public DbTableDao getDbTableDao()
        {
            if (dbTableDao == null)
            {
                dbTableDao = new DbTableDaoImpl();
            }
            return dbTableDao;
        }

         public UserDao getUserDao()
        {
            if (userDao == null)
            {
                userDao = new UserDaoImpl();
            }
            return userDao;
        }
    }
}