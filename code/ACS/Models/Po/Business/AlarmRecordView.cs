using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class AlarmRecordView
    {
        public const String ID = "AlarmID";
        public const String EVENTTYPEID = "EventTypeID";
        public const String DOORNAME = "DoorName";
        public const String ALARMTIME = "AlarmTime";
        //从AlarmRecord表中获得
        private int alarmID;                //时间ID
        private DateTime alarmTime;         //时间时间
        private int doorID;                 //门ID
        private int controlID;              //控制器ID
        private int eventTypeID;           //事件类型
        public virtual String EventTypeName { get; set; }                   //事件名称
        public virtual String ForegroundColor { get; set; }                 //前景色
        public virtual String BackgroundColor { get; set; }                 //背景色
        //从Control表中获得
        private String controlName;         //控制器名称

        //从Door表中获得
        private String doorName;            //门名称


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

        public virtual String ControlName
        {
            get { return controlName; }
            set { controlName = value; }
        }

        public virtual String DoorName
        {
            get { return doorName; }
            set { doorName = value; }
        }
    }
}