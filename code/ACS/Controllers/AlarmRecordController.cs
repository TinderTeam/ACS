using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Common.Model;
using ACS.Service;
using ACS.Models.Po.Business;
using System.Web.Script.Serialization;
using ACS.Common.Util;
using ACS.Common.Dao;
namespace ACS.Controllers
{
    public class AlarmRecordController : MiniUITableController<AlarmRecord>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        AlarmRecordService alarmRecordService = ServiceContext.getInstance().getAlarmRecordService();

        public override CommonService<AlarmRecord> getService()
        {
            return alarmRecordService;
        }
        //用于实现条件查询功能
        public override List<QueryCondition> GetFilterCondition(String json)
        {
            return null;
        }

    }
}
