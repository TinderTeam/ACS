using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace ACS.Common.Dao.impl
{
    public abstract class AbstractDao<E> : AbstractViewDao<E>, Dao<E>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void create(E obj)
        {
            
		    log.Info("the object class is " + getFeaturedClass()+"the object is "+ obj.ToString());
            ISession session = null;
            try
            {

                session = SessionManager.getInstance().GetSession();
                ITransaction tx = session.BeginTransaction();

                session.Save(obj);

                tx.Commit();


            }
            catch (System.Exception ex)
            {

                log.Error("create object", ex);
                throw ex;
            }
            finally
            {
                if (null != session)
                {
                    session.Close();
                }
            }
        }

        public void update(E obj)
        {
            throw new NotImplementedException();
        }

        public void delete(E obj)
        {
            throw new NotImplementedException();
        }

        public void delete(QueryCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}