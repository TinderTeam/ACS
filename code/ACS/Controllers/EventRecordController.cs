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
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EventRecordView.DEPTNAME, eventRecordFilter.DeptName));
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EventRecordView.JOBNAME, eventRecordFilter.JobName));
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EventRecordView.DOORNAME, eventRecordFilter.DoorName));
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EventRecordView.CARDNO, eventRecordFilter.CardNo));
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EventRecordView.EMPLOYEENAME, eventRecordFilter.EmployeeName));
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EventRecordView.EMPLOYEECODE, eventRecordFilter.EmployeeCode));
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.EQUAL, EventRecordView.EVENTTYPEID , eventRecordFilter.EventTypeID.ToString()));
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

    }
}
