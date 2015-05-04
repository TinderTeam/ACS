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
        //加载报警记录类型列表
        public ActionResult LoadEventTypeList()
        {
            List<EventTypeModel> typeList = alarmRecordService.GetEventTypeList();
            return ReturnJson(typeList);
        }
        //加载报警信息列表
        public override ActionResult Load(String data)
        {
            return LoadTable<AlarmRecordView>(GetFilterCondition(data));
        }
        //用于实现条件查询功能
        public override List<QueryCondition> GetFilterCondition(String json)
        {
            log.Debug("Loading AlarmRecord... ,the FilterCondition is " + json);
            List<QueryCondition> filterCondition = new List<QueryCondition>();
            AlarmRecordFilterModel alarmRecordFilter = JsonConvert.JsonToObject<AlarmRecordFilterModel>(json);
            if (null != alarmRecordFilter)
            {
                if (!ValidatorUtil.isEmpty(alarmRecordFilter.EventTypeID))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AlarmRecordView.EVENTTYPEID, alarmRecordFilter.EventTypeID.ToString()));
                }
                if (!ValidatorUtil.isEmpty(alarmRecordFilter.DoorID))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AlarmRecordView.DOORID, alarmRecordFilter.DoorID));
                }

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
        class AlarmRecordFilterModel
        {
            public DateTime AlarmTimeStart { get; set; }    //查询起始时间
            public DateTime AlarmTimeEnd { get; set; }      //查询截至时间
            public String EventTypeID { get; set; }         //事件类型ID
            public String DoorName { get; set; }            //门名称
            public String DoorID { get; set; }            //门名称
        }
    }
}
