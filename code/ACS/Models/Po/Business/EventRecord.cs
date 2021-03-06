﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class EventRecord
    {
        public const String ID = "EventID";
        private int eventID;                //事件ID
        private DateTime eventTime;         //事件时间
        private String cardNo;                 //卡ID
        //更改：这里要冗余一份员工的信息，如果卡ID未找到员工则只记录卡，
        private int employeeID;
        private int doorID;                 //门ID
        private int eventTypeID;              //事件类型ID
        private int controlID;              //控制器ID
        private int modify;                 //是否修改过

        public virtual int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        public virtual int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }
        public virtual String CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
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
        public virtual int EventTypeID
        {
            get { return eventTypeID; }
            set { eventTypeID = value; }
        }
        public virtual int Modify
        {
            get { return modify; }
            set { modify = value; }
        }

    }
}