using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class EventRecord
    {
        private int eventID;                //事件ID
        private DateTime eventTime;         //事件时间
        private int employeeID;             //员工ID
        private int doorID;                 //门ID
        private String eventType;           //事件类型
        private int controlID;              //控制器ID
        private String modify;              //是否修改过

        public virtual int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }
        public virtual int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }
        public virtual DateTime EventTime
        {
            get { return eventTime; }
            set { eventTime = value; }
        }
        public virtual int ControlID
        {
            get { return controlID; }
            set { controlID = value; }
        }
        public virtual int DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }
        public virtual String EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }
        public virtual String Modify
        {
            get { return modify; }
            set { modify = value; }
        }

    }
}