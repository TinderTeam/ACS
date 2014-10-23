using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace ACS.Common.Dao.impl
{
    public class DaoCommonImpl<E> : ViewDaoCommonImpl<E>, Dao<E>
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
            ISession session = null;
            try
            {
                session = SessionManager.getInstance().GetSession();
                ITransaction tx = session.BeginTransaction();

                session.Update(obj);

                tx.Commit();

            }
            catch (System.Exception ex)
            {

                log.Error("update user error", ex);
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

        public void delete(E obj)
        {
            
        }

        public void delete(QueryCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}