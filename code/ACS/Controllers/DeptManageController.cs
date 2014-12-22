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
    public class DeptManageController : MiniUITableController<Dept>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        DeptService deptService = ServiceContext.getInstance().getDeptService();

        public override CommonService<Dept> getService()
        {
            return this.deptService;
        }
    }
}
