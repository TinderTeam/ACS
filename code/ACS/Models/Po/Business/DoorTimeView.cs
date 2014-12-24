using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class DoorTimeView
    {

        //从DoorTime中获取
        public const String DOOR_TIME_ID = "DoorTimeID";
        public const String CONTROL_ID = "ControlID";
        public const String ENABLE = "Enable";
        public const String DISABLE = "Disable";

        private int doorTimeID;             //开放时间索引
        private int doorTimeNum;         //开放时间序号

        private int doorID;                 //门索引
        private int doorNum;                //门编号
     
        private String doorTimeName;        //开放时间名称
        public virtual String StartTime { get; set; }          //开始时间
        public virtual String EndTime { get; set; }             //截止时间
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

        //根据接口补充加入
        public virtual int Identify { get; set; }       //识别方式
        public virtual DateTime LimitDate { get; set; }   //到期日期
      
        public virtual int DoorTimeID
        {
            get { return doorTimeID; }
            set { doorTimeID = value; }
        }

        public virtual int DoorNum
        {
            get { return doorNum; }
            set { doorNum = value; }
        }

        public virtual int DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }


        public virtual int DoorTimeNum
        {
            get { return doorTimeNum; }
            set { doorTimeNum = value; }
        }
        public virtual String DoorTimeName
        {
            get { return doorTimeName; }
            set { doorTimeName = value; }
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