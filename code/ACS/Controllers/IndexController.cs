using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACM.Controllers
{
    public class IndexController : Controller
    {
        /// <summary>
        /// 首页显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Overview()
        {
            return View();
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Exit()
        {
            return RedirectToAction("Login", "Index");
        }
       
        public String MenuTree()
        {
            return "[{id:\"user\", text: \"用户管理\"},{id:\"UserManage\", text:\"用户管理\", pid: \"user\" }]";
        }
    }
}
