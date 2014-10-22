using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;


namespace ACS.Common.Dao.impl
{
    public abstract class AbstractViewDao<E> : ViewDao<E>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Type getFeaturedClass()
        {
            return typeof(E);
        }

        public List<E> getAll()
        {
            throw new NotImplementedException();
        }

        public long getCount(List<QueryCondition> conditionList)
        {
            throw new NotImplementedException();
        }

        public List<E> getAll(List<QueryCondition> conditionList)
        {
            List<E> objectList = null;
            ISession session = null;
            try
            {
                session = SessionManager.getInstance().GetSession();
 
                ICriteria c = SessionManager.getCriteriaByCondition(this.getFeaturedClass(), conditionList, session);

                var queryList = c.List<E>();
                foreach (var result in queryList)
                {
                    objectList.Add(result);
                }


            }
            catch (System.Exception re)
            {
                log.Error("getAll error", re);

                throw re;
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }

            log.Info("the object calss is " + getFeaturedClass() + "the object list size is " + objectList.Count);
            return objectList;
        }

        public List<E> getAll(List<QueryCondition> conditionList, int startNum, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<E> getAll(QueryCondition condition)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();

            if (null != condition)
            {
                conditionList.Add(condition);
            }
            return getAll(conditionList);
        }

        public E getUniRecord(QueryCondition condition)
        {
            throw new NotImplementedException();
        }

        public E getUniRecord(List<QueryCondition> conditionList)
        {
            throw new NotImplementedException();
        }
    }
}