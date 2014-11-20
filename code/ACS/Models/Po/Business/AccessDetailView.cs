using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class AccessDetailView
    {
        //AccessDetail表中获取
        public const String ID = "AccessID";
        private int accessDetailID;         //门禁权限详细信息ID
        private int accessID;               //门禁权限ID
        private int valueID;                //子AccessID或者DoorTime ID
        private string type;                //类型 Access/DoorTime

        //父Access表中获取
        private String accessName;          //父门禁权限名称

        //Control表中获取
        private int controlID;              //控制器ID
        private String controlName;         //控制器名称

        //Door表中获取
        private int doorID;                 //门ID
        private String doorName;            //门名称

        //DoorTime标准获取
        private String doorTimeName;        //门时间段名称
        private String startTime;         //开始时间
        private String endTime;           //截止时间

        public virtual int AccessDetailID
        {
            get { return accessDetailID; }
            set { accessDetailID = value; }
        }

        public virtual int AccessID
        {
            get { return accessID; }
            set { accessID = value; }
        }

        public virtual int ValueID
        {
            get { return valueID; }
            set { valueID = value; }
        }

        public virtual string Type
        {
            get { return type; }
            set { type = value; }
        }

        public virtual String AccessName
        {
            get { return accessName; }
            set { accessName = value; }
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

        public virtual int DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }

        public virtual String DoorName
        {
            get { return doorName; }
            set { doorName = value; }
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