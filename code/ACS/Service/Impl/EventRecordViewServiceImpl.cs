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
using ACS.Common.Model;
using ACS.Common.Util;
namespace ACS.Service.Impl
{
    public class EventRecordServiceImpl : CommonServiceImpl<EventRecord>, EventRecordService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ViewDao<EventRecordView> eventRecordViewDao = DaoContext.getInstance().getEventRecordViewDao();

        //获取对象信息
        public override PersistenceObjInfo GetObjectInfo()
        {
            PersistenceObjInfo perObjInfo = new PersistenceObjInfo();
            perObjInfo.PrimaryName = EventRecord.ID;
            return perObjInfo;
        }

        public List<EventRecordView> GetCurEvent(String indexID, String doorID)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            /*这里不使用门的过滤条件*/
            //conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, "DoorID", doorID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.DESC_ORDER, EventRecordView.ID));
            List<EventRecordView> alarmList;
            if (indexID.Equals("0"))
            {
                alarmList = this.GetDao<EventRecordView>().getAll(conditionList, 0, 1);
            }
            else
            {
                conditionList.Add(new QueryCondition(ConditionTypeEnum.BIGER, EventRecordView.ID, indexID.ToString()));
                alarmList = this.GetDao<EventRecordView>().getAll(conditionList);
            }
            return alarmList;
        }
        //获取刷卡记录事件类型列表
        public List<EventTypeModel> GetEventTypeList()
        {
            List<EventTypeModel> eventTypeModelList = new List<EventTypeModel>();
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL,EventType.VISIBLE, "true");
            List<EventType> eventTypeList = GetDao<EventType>().getAll(condition);
            if(!ValidatorUtil.isEmpty<EventType>(eventTypeList))
            {
                foreach (EventType eventType in eventTypeList)
                {
                    EventTypeModel eventTypeModel = new EventTypeModel();
                    eventTypeModel.EventTypeID = eventType.EventTypeID;
                    eventTypeModel.EventTypeName = eventType.EventTypeName;
                    eventTypeModelList.Add(eventTypeModel);
                }
            }

            return eventTypeModelList;
        }

    }
}