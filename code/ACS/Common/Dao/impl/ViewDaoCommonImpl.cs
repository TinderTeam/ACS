using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using ACS.Common.Constant;
using ACS.Common.Util;


namespace ACS.Common.Dao.impl
{
    public  class ViewDaoCommonImpl<E> : ViewDao<E>
    {
        
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Type getFeaturedClass()
        {
            return typeof(E);
        }

        public List<E> getAll()
        {
            List<E> objectList = new List<E>();
            ISession session = null;
            try
            {
                session = SessionManager.getInstance().GetSession();

                ICriteria c = session.CreateCriteria(this.getFeaturedClass());

                var queryList = c.List<E>();
                foreach (var result in queryList)
                {
                    objectList.Add(result);
                }


            }
            catch (System.Exception re)
            {
                log.Error("getAll error", re);

                throw new FuegoException(FuegoCommonMsg.DATABASE_FAIL, re);
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

        public long getCount(List<QueryCondition> conditionList)
        {
            long count = 0;
            ISession session = null;
            try
            {
                session = SessionManager.getInstance().GetSession();
                ICriteria c = SessionManager.getCriteriaByCondition(this.getFeaturedClass(), conditionList, session);
                count = Convert.ToInt64(c.SetProjection(Projections.RowCount()).UniqueResult());
            }
            catch (System.Exception re)
            {
                log.Error("getAll error", re);

                throw new FuegoException(FuegoCommonMsg.DATABASE_FAIL, re);
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }

            log.Info("the object calss is " + getFeaturedClass() + "the count is " + count);
            return count;

        }

        public List<E> getAll(List<QueryCondition> conditionList)
        {
            List<E> objectList = new List<E>();
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

                throw new FuegoException(FuegoCommonMsg.DATABASE_FAIL, re);
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
        private bool isContainsFalse(List<QueryCondition> conditionList)
        {
            if (!ValidatorUtil.isEmpty<QueryCondition>(conditionList))
            {
                foreach (QueryCondition e in conditionList)
                {
                    if (e.ConditionType == ConditionTypeEnum.FALSE)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public List<E> getAll(List<QueryCondition> conditionList, int startNum, int pageSize)
        {
            List<E> objectList = new List<E>();

            if (isContainsFalse(conditionList))
            {
                log.Warn("the condition contains false,no need to query");
                return objectList;
            }
            ISession session = null;
            try
            {
                session = SessionManager.getInstance().GetSession();
                ICriteria c = SessionManager.getCriteriaByCondition(this.getFeaturedClass(), conditionList, session);

                c.SetFirstResult(startNum);
                c.SetMaxResults(pageSize);
                var queryList = c.List<E>();
                foreach (var result in queryList)
                {
                    objectList.Add(result);
                }
            }
            catch (System.Exception re)
            {
                log.Error("getAll error", re);

                throw new FuegoException(FuegoCommonMsg.DATABASE_FAIL, re);
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }

            log.Info("the object calss is " + getFeaturedClass() + "the object list size is " + objectList.Count());
            return objectList;
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
            log.Info("the condition is " + condition);
            E record;
            ISession session = null;
            try
            {
                session = SessionManager.getInstance().GetSession();

                List<QueryCondition> conditionList = new List<QueryCondition>();

                if (null != condition)
                {
                    conditionList.Add(condition);
                }
                ICriteria c = SessionManager.getCriteriaByCondition(this.getFeaturedClass(), conditionList, session);
                record = (E)c.UniqueResult();
            }
            catch (System.Exception re)
            {
                log.Error("get UniRecord error", re);
                throw new FuegoException(FuegoCommonMsg.DATABASE_FAIL, re);
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }

            log.Info("the object calss is " + getFeaturedClass() + "the object is " + record);

            return record;
        }

        public E getUniRecord(List<QueryCondition> conditionList)
        {
            E record;
            ISession session = null;
            try
            {
                session = SessionManager.getInstance().GetSession();

                ICriteria c = SessionManager.getCriteriaByCondition(this.getFeaturedClass(), conditionList, session);
                record = (E)c.UniqueResult();
            }
            catch (System.Exception re)
            {
                log.Error("get UniRecord error", re);

                throw new FuegoException(FuegoCommonMsg.DATABASE_FAIL, re);
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
            }

            log.Info("the object calss is " + getFeaturedClass() + "the object is " + record);

            return record;
        }


    }
}