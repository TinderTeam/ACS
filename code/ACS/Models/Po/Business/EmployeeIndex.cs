using ACS.Common;
using ACS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class EmployeeIndex
    {
        public virtual int EmployeeIndexID { get; set; }
        public virtual int EmployeeID { get; set; }
        public virtual String IndexStatus { get; set; }
    }
}