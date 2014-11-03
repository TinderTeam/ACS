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
    public class AlarmRecordServiceImpl : AlarmRecordService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<AlarmRecord> alarmRecordDao = DaoContext.getInstance().getAlarmRecordDao();

        public AbstractDataSource<AlarmRecord> getAlarmRecordList(AlarmRecord filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<AlarmRecord> dataSource = new DatabaseSourceImpl<AlarmRecord>(conditionList);
            return dataSource;
        }
        
    }
}