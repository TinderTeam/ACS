using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class DoorTime
    {

        public const string DOOR_ID = "DoorID";

        private int doorTimeID;//开放时间编号
        private int doorID;//门编号
        private String doorTimeName;//开放时间名称
        private String startTime;//开始时间
        private String endTime;//截止时间
        private string enable;

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