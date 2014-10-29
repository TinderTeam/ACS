using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACM.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 欢迎页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
           // return RedirectToAction("Login", "Index");
            return RedirectToAction("Index", "Index");
        }

        public ActionResult Test()
        {
            return RedirectToAction("UserManage", "UserManage");
        }


    }
}
