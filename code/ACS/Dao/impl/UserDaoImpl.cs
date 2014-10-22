using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po;
using NHibernate;
using NHibernate.Criterion;
using ACS.Test;
namespace ACS.Dao.impl
{
    public class UserDaoImpl : UserDao
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<User> getAll()
        {
            return Stub.getUserList();
            //To be Implemented
        }
    }
}