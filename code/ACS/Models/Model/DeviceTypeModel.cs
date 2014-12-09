using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACS.Models.Model
{
    public class DeviceTypeModel
    {
        public int TypeID { get; set; }
        public String TypeName { get; set; }
        public int DoorNum { get; set; }
        public int TimeNum { get; set; }
    }
}
