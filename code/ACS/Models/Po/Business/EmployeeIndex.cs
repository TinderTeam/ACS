using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class EmployeeIndex
    {
        public const String RECEIVE = "Receive";
        public const String OK = "OK";
        public const String INDEX_STATUS = "IndexStatus";
        public const String EMPLOYEE_INDEX_ID = "EmployeeIndexID";
        public const String EMPLOYEE_ID = "EmployeeID";
        
        public virtual int EmployeeIndexID { get; set; }
        public virtual int EmployeeID { get; set; }
        private  String indexStatus = "OK";

        public virtual String IndexStatus
        {
            get { return indexStatus; }
            set { indexStatus = value; }
        }

       
    }
}