using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Test;
namespace ACS.Controllers
{
    public class MonitorController : Controller
    {
        //
        // GET: /Monitor/

        public ActionResult Monitor()
        {
            return View();
        }


        public String DeviceTree(String key)
        {
            TreeModel tree = Stub.getTestDeviceTree();
            String str = tree.ToJsonStr();
            return str;
        }
    }
}
