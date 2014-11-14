using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Test;

namespace ACS.Controllers
{
    public class AccessManageController : Controller
    {
        //
        // GET: /AccessManage/

        public ActionResult AccessManage()
        {
            return View();
        }

        /// <summary>
        /// Load access list
        /// </summary>
        /// <returns></returns>
        public String AccessList()
        {

            TreeModel tree = Stub.getTestAccessList();
            String str = tree.ToJsonStr();
            return str;
        }
        
        /// <summary>
        /// Load access detail
        /// </summary>
        /// <param name="AccessID"></param>
        /// <returns></returns>
        public ActionResult AccessDetail(String AccessID)
        {
            return View();
        }

        /// <summary>
        /// Load access detail
        /// </summary>
        /// <param name="AccessID"></param>
        /// <returns></returns>
        public ActionResult AccessEdit()
        {
            return View();
        }
    }
}
