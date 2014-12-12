using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class DoorTimeView
    {

        //从DoorTime中获取
        private int doorTimeID;             //开放时间编号
        private int doorID;                 //门编号
        private String doorTimeName;        //开放时间名称
        private String startTime;           //开始时间
        private String endTime;             //截止时间
        private string enable;              //是否使能
        public virtual bool Monday { get; set; }
        public virtual bool Tuesday { get; set; }
        public virtual bool Wednesday { get; set; }
        public virtual bool Thursday { get; set; }
        public virtual bool Friday { get; set; }
        public virtual bool Saturday { get; set; }
        public virtual bool Sunday { get; set; }
        public virtual bool Holiday { get; set; }
        //从Door中获取
        private String doorName;            //门名称

        //从Control中获取
        private int controlID;              //控制器ID
        private String controlName;         //控制器名称

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


        public virtual string Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        public virtual String DoorName
        {
            get { return doorName; }
            set { doorName = value; }
        }

        public virtual int ControlID
        {
            get { return controlID; }
            set { controlID = value; }
        }

        public virtual String ControlName
        {
            get { return controlName; }
            set { controlName = value; }
        }
    }
}