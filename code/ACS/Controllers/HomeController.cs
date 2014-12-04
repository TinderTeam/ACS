using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Service.Impl;
using ACS.Test;
using TcpipIntface;
namespace ACS.Controllers
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
            //------     Test   --------------------
            UserModel loginUser = Stub.getUser();
            Session["SystemUser"] = loginUser;
            //---------------------
            return RedirectToAction("Index", "Index");
        }

        public ActionResult Test()
        {
            return RedirectToAction("UserManage", "UserManage");
        }


    }
}
