using ACS.Common;
using ACS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class Job : LogOperable
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

        public virtual string GetObjType()
        {
            return ServiceConstant.LOG_OBJ_JOB;
        }

        public virtual string GetObjName()
        {
            return this.jobName;
        }

        public virtual string GetDesp()
        {
            return null;
        }
    }
}