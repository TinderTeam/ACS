using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACM.Controllers
{
    public class PersonalManageController : Controller
    {
        /// <summary>
        /// 个人信息修改
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalManage()
        {
            return View();
        }

        /// <summary>
        /// 个人密码修改
        /// </summary>
        /// <returns></returns>
        public ActionResult PswdManage()
        {
            return View();
        }

    }
}
