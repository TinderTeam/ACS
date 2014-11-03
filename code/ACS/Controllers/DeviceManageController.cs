using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Test;

namespace ACS.Controllers
{
    public class DeviceManageController : Controller
    {
        //
        // GET: /Device/

        public ActionResult DeviceManage()
        {
            return View();
        }

        public String DeviceTree(String key)
        {
            TreeModel tree = Stub.getTestDeviceTree();
            String str = tree.ToJsonStr();
            return str;
        }

        public String DoorTree(String key)
        {
            TreeModel tree = Stub.getTestDoorTree();
            String str = tree.ToJsonStr();
            return str;
        }


    }
}
