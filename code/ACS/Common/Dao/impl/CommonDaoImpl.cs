using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Util;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace ACS.Common.Dao.impl
{
    public class CommonDaoImpl<E> : ViewDaoCommonImpl<E>, CommonDao<E>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void create(E obj)
        {
            log.Info("the object class is " + getFeaturedClass());
            if (null == obj)
            {
                log.Warn("the object is null.");
            }

		    log.Info("the object is "+ obj.ToString());

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

 

        public void create(List<E> objList)
        {
            log.Info("the object class is " + getFeaturedClass());
            if(ValidatorUtil.isEmpty<E>(objList))
            {
                log.Warn("the object list is empty");
                return ;
            }

            log.Info( "the object list count is  " + objList.Count);
            ISession session = null;
            try
            {

                session = SessionManager.getInstance().GetSession();
                ITransaction tx = session.BeginTransaction();
                foreach(E e in objList)
                {
                    session.Save(e);
                }
 
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
            log.Info("the object class is " + getFeaturedClass()+"the object is "+obj.ToString());
            ISession session = null;
		    try
		    {
                session = SessionManager.getInstance().GetSession();
                ITransaction tx = session.BeginTransaction();
                session.Update(obj);

                tx.Commit();    
            }
            catch (System.Exception re)
		    {
			    log.Error("update error",re);
			    throw re;

		    } 
            finally
		    {
                if (null != session)
                {
                    session.Close();
                }
		    }
        }

        public void update(List<E> objList)
        {
            log.Info("the object class is " + getFeaturedClass());
            if (ValidatorUtil.isEmpty<E>(objList))
            {
                log.Warn("the object list is empty");
                return;
            }

            log.Info("the object list count is  " + objList.Count);
            ISession session = null;
            try
            {
                session = SessionManager.getInstance().GetSession();
                ITransaction tx = session.BeginTransaction();
                foreach(E obj in objList)
                {
                    session.Update(obj);
                }
                tx.Commit();
                
            }
            catch (System.Exception re)
            {
                log.Error("update error", re);
                throw re;

            }
            finally
            {
                if (null != session)
                {
                    session.Close();
                }
            }
        }

        public void delete(QueryCondition condition)
        {
            log.Info("the query is " + condition.ToString());
	
		    List<QueryCondition> conditionList = new List<QueryCondition>();
		
		    if(null != condition)
		    {
			    conditionList.Add(condition);
		    }ISession session = null;
		
		
		    ITransaction tx = null;
		    try
		    {
			    session = SessionManager.getInstance().GetSession();
			    tx = session.BeginTransaction();
                ICriteria c = SessionManager.getCriteriaByCondition(this.getFeaturedClass(), conditionList, session);
			    
			    foreach(var obj in c.List())
			    {
				    session.Delete(obj);
			    }


			    tx.Commit();
            }
            catch (System.Exception re)
		    {
			    log.Error("delete error",re);
			    throw re;

		    } finally
		    {
			    if (session != null)
			    {
				    session.Close();
			    }
		    }
        }

        public void delete(List<QueryCondition> conditionList)
        {
            log.Info("the query is " + conditionList.ToString());


            ISession session = null;


            ITransaction tx = null;
            try
            {
                session = SessionManager.getInstance().GetSession();
                tx = session.BeginTransaction();
                ICriteria c = SessionManager.getCriteriaByCondition(this.getFeaturedClass(), conditionList, session);

                foreach (var obj in c.List())
                {
                    session.Delete(obj);
                }


                tx.Commit();
            }
            catch (System.Exception re)
            {
                log.Error("delete error", re);
                throw re;

            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }
        }

    }
}