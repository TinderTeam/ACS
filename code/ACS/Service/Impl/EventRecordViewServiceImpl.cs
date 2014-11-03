using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Dao;
using ACS.Common.Dao.datasource;
using ACS.Common.Dao;
using ACS.Common.Constant;
using ACS.Service.Constant;
namespace ACS.Service.Impl
{
    public class EventRecordViewServiceImpl : EventRecordViewService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<EventRecordView> eventRecordViewDao = DaoContext.getInstance().getEventRecordViewDao();

        public AbstractDataSource<EventRecordView> getEventRecordViewList(EventRecordView filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<EventRecordView> dataSource = new DatabaseSourceImpl<EventRecordView>(conditionList);
            return dataSource;
        }
        
    }
}