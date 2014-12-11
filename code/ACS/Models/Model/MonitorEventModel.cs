using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class MonitorEventModel
    {
        public int Id { get; set; }
        public DateTime EventTime { get; set; }
        public String DoorName { get; set; }
        public int DoorID { get; set; }
        public String ControlName { get; set; }
        public int ContorID { get; set; }
        public String CardNo { get; set; }
        public String EventType { get; set; }
        public String EmployeeName{ get; set; }
        public String Img{ get; set; }
 
    }
}