using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Common.Util;
using ACS.Controllers.Constant;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Service;

namespace ACS.Controllers
{
    public class JobManageController : MiniUITableController<Job>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        JobService jobService = ServiceContext.getInstance().getJobService();

        public override CommonService<Job> getService()
        {
            return this.jobService;
        }
 
    }
}
