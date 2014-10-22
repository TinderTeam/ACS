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
    public class DbTableDaoImpl : DbTableDao
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<DbTable> getTableByName(String name)
        {
            return Stub.getDbTableList();
            /*
            List<DbTable> dbTableList = new List<DbTable>();
            ISession session = null;
            try
            {
                session = SessionManager.getInstance().GetSession();
                ITransaction tx = session.BeginTransaction();

                ICriteria criteria = session.CreateCriteria<DbTable>();

                criteria.Add(Restrictions.Eq("TableName", name));

                var queryList = criteria.List<DbTable>();
                foreach (var result in queryList)
                {
                    dbTableList.Add(result);
                }

            }
            catch (System.Exception ex)
            {

                log.Error("search user error", ex);
                throw ex;
            }
            finally
            {
                if (null != session)
                {
                    session.Close();
                }
            }
            return dbTableList;

             */

        }
    }
}