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
        public ActionResult  AccessSelect(String doorID)
        {
            return View();
        }

        /// <summary>
        /// 移除门禁权限
        /// </summary>
        /// <param name="idstr">选中的门禁权限ID,用','分割</param>
        /// <returns>ajax返回</returns>
        public ActionResult Remove(String idstr)
        {
            return null;
        }

        /// <summary>
        /// 选择门禁权限
        /// </summary>
        /// <returns></returns>

        public ActionResult select()
        {
            return null;
        }
    }
}
