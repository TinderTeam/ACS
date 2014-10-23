using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
namespace ACS.Dao
{
    public class SessionManager
    {
        private static SessionManager instance;
        private readonly ISessionFactory sessionFactory;
        private SessionManager()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
        }
        public static SessionManager getInstance()
        {
            if (instance == null)
            {
                instance = new SessionManager();
            }
            return instance;
        }
        public ISession GetSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}