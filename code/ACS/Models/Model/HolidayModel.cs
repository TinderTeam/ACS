using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class HolidayModel
    {
        private int holidayID;//假日ID
        private String holidayName;//假日名称
        private String holidayNote;//假日说明
        private DateTime startTime;//开始时间
        private DateTime endTime;//结束时间

        public int HolidayID
        {
            get { return holidayID; }
            set { holidayID = value; }
        }
        
        public String HolidayName
        {
            get { return holidayName; }
            set { holidayName = value; }
        }
        
        public String HolidayNote
        {
            get { return holidayNote; }
            set { holidayNote = value; }
        }
        
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        
    }
}