using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class DoorTime
    {

        public const String DOOR_ID = "DoorID";
        public const String DOOR_TIME_ID = "DoorTimeID";

        private int doorTimeID;                     //开放时间编号
        private int doorID;                         //门编号
        private String doorTimeName;                //开放时间名称
        private String startTime;                   //开始时间
        private String endTime;                     //截止时间
        private String enable = "disable";          //是否使能
        private bool monday = false;
        private bool tuesday = false;
        private bool wednesday = false;
        private bool thursday = false;
        private bool friday = false;
        private bool saturday = false;
        private bool sunday = false;
        private bool holiday = false;
        public virtual int DoorTimeNum { get; set; }

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

        public virtual String StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public virtual String EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

    }
}