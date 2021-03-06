﻿using ACS.Common;
using ACS.Common.Util;
using ACS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class DoorTime : LogOperable
    {

        public const String DOOR_ID = "DoorID";
        public const String DOOR_TIME_ID = "DoorTimeID";

        private int doorTimeID;                     //开放时间编号
        private int doorID;                         //门编号
        private String doorTimeName;                //开放时间名称
        private DateTime startTime = Convert.ToDateTime("2000-01-01 00:00:00");  //开始时间
        private DateTime endTime = Convert.ToDateTime("2000-01-01 00:00:00");    //截止时间

        private String enable = "Disable";          //是否使能
        private bool monday = false;
        private bool tuesday = false;
        private bool wednesday = false;
        private bool thursday = false;
        private bool friday = false;
        private bool saturday = false;
        private bool sunday = false;
        private bool holiday = false;

        public virtual int DoorTimeNum { get; set; }
        //根据接口补充加入

        public virtual int Identify { get; set; }       //识别方式
        private  DateTime limitDate = Convert.ToDateTime("2099-01-01 00:00:00");  //到期日期默认未到期


        public virtual DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public virtual DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public virtual  DateTime LimitDate
        {
            get { return limitDate; }
            set { limitDate = value; }
        }

        public virtual bool Monday
        {
            get { return monday; }
            set { monday = value; }
        }

        public virtual bool Tuesday
        {
            get { return tuesday; }
            set { tuesday = value; }
        }

        public virtual bool Wednesday
        {
            get { return wednesday; }
            set { wednesday = value; }
        }

        public virtual bool Thursday
        {
            get { return thursday; }
            set { thursday = value; }
        }

        public virtual bool Friday
        {
            get { return friday; }
            set { friday = value; }
        }

        public virtual bool Saturday
        {
            get { return saturday; }
            set { saturday = value; }
        }

        public virtual bool Sunday
        {
            get { return sunday; }
            set { sunday = value; }
        }

        public virtual bool Holiday
        {
            get { return holiday; }
            set { holiday = value; }
        }

        public virtual string Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        public virtual int DoorTimeID
        {
            get { return doorTimeID; }
            set { doorTimeID = value; }
        }
        
        public virtual int DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }
        
        public virtual String DoorTimeName
        {
            get { return doorTimeName; }
            set { doorTimeName = value; }
        }



        public virtual string GetObjType()
        {
            return ServiceConstant.LOG_OBJ_DOORTIME;
        }

        public virtual string GetObjName()
        {
            return this.doorTimeName;
        }

        public virtual string GetDesp()
        {
            return null;
        }
    }
}