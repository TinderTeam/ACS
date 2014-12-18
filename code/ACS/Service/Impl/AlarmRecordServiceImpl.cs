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
namespace ACS.Service.Impl
{
    public class AlarmRecordServiceImpl : CommonServiceImpl<AlarmRecord>, AlarmRecordService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
 
        //获取对象信息
        public override PersistenceObjInfo GetObjectInfo()
        {
            PersistenceObjInfo perObjInfo = new PersistenceObjInfo();
            perObjInfo.PrimaryName = AlarmRecord.ID;
            return perObjInfo;
        }

        public List<AlarmRecordView> GetCurAlarm(String indexID, String doorID)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            /*这里使用用门的过滤条件*/
            //conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, "DoorID", doorID));

            conditionList.Add(new QueryCondition(ConditionTypeEnum.DESC_ORDER, AlarmRecordView.ID));


            List<AlarmRecordView> alarmList;
            if (indexID.Equals("0"))
            {
                alarmList = this.GetDao<AlarmRecordView>().getAll(conditionList, 0, 1);
            }
            else
            {
                conditionList.Add(new QueryCondition(ConditionTypeEnum.BIGER, AlarmRecordView.ID, indexID.ToString()));
                alarmList = this.GetDao<AlarmRecordView>().getAll(conditionList);
            }
            return alarmList;
        }
    }
}