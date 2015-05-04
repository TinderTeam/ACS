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
    public class EventRecordController : MiniUITableController<EventRecord>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        EventRecordService eventRecordService = ServiceContext.getInstance().getEventRecordService();

        public override CommonService<EventRecord> getService()
        {
            return eventRecordService;
        }

        //加载刷卡记录类型列表
        public ActionResult LoadEventTypeList()
        {
            List<EventTypeModel> typeList = eventRecordService.GetEventTypeList();
            return ReturnJson(typeList);
        }

        //加载刷卡记录列表
        public override ActionResult Load(String data)
        {
            return LoadTable<EventRecordView>(GetFilterCondition(data));
        }

        //用于实现条件查询功能
        public override List<QueryCondition> GetFilterCondition(String json)
        {
            List<QueryCondition> filterCondition = new List<QueryCondition>();
            EventRecordFilterModel eventRecordFilter = JsonConvert.JsonToObject<EventRecordFilterModel>(json);
            if (null != eventRecordFilter)
            {
                if (!ValidatorUtil.isEmpty(eventRecordFilter.DeptID))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.EQUAL, EventRecordView.DEPTID, eventRecordFilter.DeptID));
                }

                if (!ValidatorUtil.isEmpty(eventRecordFilter.JobID))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.EQUAL, EventRecordView.JOBID, eventRecordFilter.JobID));
                }

                if (!ValidatorUtil.isEmpty(eventRecordFilter.DoorID))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.EQUAL, EventRecordView.DOORID, eventRecordFilter.DoorID));
                }
                if (!ValidatorUtil.isEmpty(eventRecordFilter.CardNo))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EventRecordView.CARDNO, eventRecordFilter.CardNo));
                }
                if (!ValidatorUtil.isEmpty(eventRecordFilter.EmployeeName))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EventRecordView.EMPLOYEENAME, eventRecordFilter.EmployeeName));
                }
                if (!ValidatorUtil.isEmpty(eventRecordFilter.EmployeeCode))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EventRecordView.EMPLOYEECODE, eventRecordFilter.EmployeeCode));
                }
              
               
               
               

                if (!ValidatorUtil.isEmpty(eventRecordFilter.EventTypeID))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.EQUAL, EventRecordView.EVENTTYPEID, eventRecordFilter.EventTypeID));
                }

                if (!DateUtil.DateTimeIsEmpty(eventRecordFilter.EventTimeStart))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.BIGER_EQ, EventRecordView.EVNETTIME, DateUtil.DateTimeToString(eventRecordFilter.EventTimeStart)));
                }

                if (!DateUtil.DateTimeIsEmpty(eventRecordFilter.EventTimeEnd))
                {
                    filterCondition.Add(new QueryCondition(ConditionTypeEnum.LOWER_EQ, EventRecordView.EVNETTIME, DateUtil.DateTimeToString(eventRecordFilter.EventTimeEnd)));
                }

            }
            return filterCondition;
        }

        class EventRecordFilterModel
        {
            //从EventRecord表中获得
            public String DeptID { get; set; }
            public String JobID { get; set; }
            public String DoorName { get; set; }
            public DateTime EventTimeStart { get; set; }
            public DateTime EventTimeEnd { get; set; }
            public String CardNo { get; set; }
            public String EmployeeName { get; set; }
            public String EmployeeCode { get; set; }
            public String EventTypeID { get; set; }
            public String DoorID { get; set; }
        }

    }
}
