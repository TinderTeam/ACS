using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class DoorTimeModel
    {
        public const string DOOR_ID = "DoorID";

        private string _id;

        public string _Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _uid;

        public string _Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        private string _status;

        public string _Status
        {
            get { return _status; }
            set { _status = value; }
        }
        
        private int doorTimeID;//开放时间编号

        public virtual int DoorTimeID
        {
            get { return doorTimeID; }
            set { doorTimeID = value; }
        }
        private int doorID;//门编号

        public virtual int DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }
        private String doorTimeName;//开放时间名称

        public virtual String DoorTimeName
        {
            get { return doorTimeName; }
            set { doorTimeName = value; }
        }

        private String enable;//开放状态

        public virtual String Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        private DateTime startTime;//开始时间

        public virtual DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        private DateTime endTime;//截止时间

        public virtual DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }


    }
}