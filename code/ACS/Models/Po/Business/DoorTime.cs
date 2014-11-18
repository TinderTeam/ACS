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
        private DateTime startTime;//开始时间
        private DateTime endTime;//截止时间

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

    }
}