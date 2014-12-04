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
using ACS.Common.Constant;
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
        //加载用户首页用户信息列表
        public override ActionResult Load(String data)
        {
            return LoadTable<AlarmRecordView>(GetFilterCondition(data));
        }
        //用于实现条件查询功能
        public override List<QueryCondition> GetFilterCondition(String json)
        {
            List<QueryCondition> filterCondition = new List<QueryCondition>();
            AlarmRecordFilterModel alarmRecordFilter = JsonConvert.JsonToObject<AlarmRecordFilterModel>(json);
            if (null != alarmRecordFilter)
            {
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, AlarmRecordView.ALARMTYPE, alarmRecordFilter.AlarmType));
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, AlarmRecordView.DOORNAME, alarmRecordFilter.DoorName));
                if (!DateUtil.DateTimeIsEmpty(alarmRecordFilter.AlarmTimeStart))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.BIGER_EQ, AlarmRecordView.ALARMTIME, DateUtil.DateTimeToString(alarmRecordFilter.AlarmTimeStart)));
                }
                if (!DateUtil.DateTimeIsEmpty(alarmRecordFilter.AlarmTimeEnd))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.LOWER_EQ, AlarmRecordView.ALARMTIME, DateUtil.DateTimeToString(alarmRecordFilter.AlarmTimeEnd)));
                }
            }
            return filterCondition;
        }

    }
}
