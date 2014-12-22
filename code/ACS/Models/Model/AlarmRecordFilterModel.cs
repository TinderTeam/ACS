using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class AlarmRecordFilterModel
    {
        //从AlarmRecord表中获得
        private DateTime alarmTimeStart;         //查询起始时间
        private DateTime alarmTimeEnd;           //查询截至时间
        private int eventTypeID;                //事件类型

        //从Door表中获得
        private String doorName;                 //门名称

        public DateTime AlarmTimeStart
        {
            get { return alarmTimeStart; }
            set { alarmTimeStart = value; }
        }

        public DateTime AlarmTimeEnd
        {
            get { return alarmTimeEnd; }
            set { alarmTimeEnd = value; }
        }

        public int EventTypeID
        {
            get { return eventTypeID; }
            set { eventTypeID = value; }
        }

        public String DoorName
        {
            get { return doorName; }
            set { doorName = value; }
        }

        
    }
}