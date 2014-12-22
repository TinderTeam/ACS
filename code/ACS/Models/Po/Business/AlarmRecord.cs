using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class AlarmRecord
    {
        public const String ID = "AlarmID";
        private int alarmID;//时间ID
        private DateTime alarmTime;//时间时间
        private int doorID;//门ID
        private int controlID;//控制器ID
        private int eventTypeID;//事件类型

        public virtual int AlarmID
        {
            get { return alarmID; }
            set { alarmID = value; }
        }

        public virtual DateTime AlarmTime
        {
            get { return alarmTime; }
            set { alarmTime = value; }
        }

        public virtual int DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }

        public virtual int ControlID
        {
            get { return controlID; }
            set { controlID = value; }
        }

        public virtual int EventTypeID
        {
            get { return eventTypeID; }
            set { eventTypeID = value; }
        }

    }
}