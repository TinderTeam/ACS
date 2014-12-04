using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class EventRecordFilterModel
    {
        //从EventRecord表中获得
        public String DeptName { get; set; }
        public String JobName { get; set; }
        public String DoorName { get; set; }
        public DateTime EventTimeStart { get; set; }
        public DateTime EventTimeEnd { get; set; }
        public String CardNo { get; set; }
        public String EmployeeName { get; set; }
        public String EmployeeCode { get; set; }
        public String EventType { get; set; }
    }
}