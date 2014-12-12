using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Po.Log;
using ACS.Service;

namespace ACS.Controllers
{
    public class LogController : MiniUITableController<SystemLog>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        LogService eventTypeService = ServiceContext.getInstance().getLogService();

        public override CommonService<SystemLog> getService()
        {
            return eventTypeService;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
