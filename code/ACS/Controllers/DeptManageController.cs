using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Test;

namespace ACS.Controllers
{
    public class DeptManageController : Controller
    {
        //
        // GET: /DeptManage/

        public ActionResult DeptManage()
        {
            return View();
        }
        public String LoadTree()
        {
            TreeModel tree = Stub.getTestDeviceTree();
            String str = tree.ToJsonStr();
            return str;
        }

        /// <summary>
        /// 保存部门树
        /// 适当时候可以按照JSON的格式对库表进进行改造
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveDeptTree()
        {
            return null;
        }
    }
}
