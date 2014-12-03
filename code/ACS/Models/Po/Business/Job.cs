using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class Job
    {
        public const String ID = "JobID";
        private int jobID;          //职位ID
        private String jobName;     //职位名称

        public virtual int JobID
        {
            get { return jobID; }
            set { jobID = value; }
        }
        
        public virtual String JobName
        {
            get { return jobName; }
            set { jobName = value; }
        }
    }
}