using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Service;
using ACS.Models.Model;
using System.Resources;
namespace ACM.Controllers

{
    public class IndexController : BaseController
    {

        PlatFormService platFormService = ServiceContext.getInstance().getPlatFormService();
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
        public ActionResult LoginCheck()
        {
            try
            {

                //loginSerivce.Login(model.UserName, model.Password);
                //Session["SystemUser"] = loginSerivce.getLoginUser(model.UserName);

                //ServiceContext.getInstance().getLogService().recordLoginLog(model.UserName, "成功", model.Os, model.Broswer);

            }
            catch (SystemException ex)
            {
                //log.Error("login failed", ex);
                //ServiceContext.getInstance().getLogService().recordLoginLog(model.UserName, "失败", model.Os, model.Broswer);
                return RedirectToAction("Login", "Index", new { msg = ex.Message });
            }

            return RedirectToAction("Index", "Index");
        }
        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Exit()
        {
            //return new RedirectResult("Index/Login");
            return RedirectToAction("Login", "Index");
        }
       
        public String MenuTree()
        {
            MenuTreeModel tree=platFormService.getMenuTreeByUserID(0);
            return "[{id:\"user\", text: \"用户管理\"},{id:\"UserManage\", text:\"用户管理\", pid: \"user\" }]";
			//return  tree.ToJsonStr();
        }
        public ActionResult Default()
        {
            return View();
        }

    }
}
