using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class Holiday
    {
        public const String ID = "HolidayID";
        private int holidayID;//假日ID
        private String holidayName;//假日名称
        private String holidayNote;//假日说明
        private DateTime startTime = DateTime.Now;//开始时间
        private DateTime endTime = DateTime.Now;//结束时间

        public virtual int HolidayID
        {
            get { return holidayID; }
            set { holidayID = value; }
        }

        public virtual String HolidayName
        {
            get { return holidayName; }
            set { holidayName = value; }
        }

        public virtual String HolidayNote
        {
            get { return holidayNote; }
            set { holidayNote = value; }
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