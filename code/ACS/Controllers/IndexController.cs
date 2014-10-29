using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Service;
using ACS.Models.Model;
using System.Resources;
using ACS.Test;
using ACS.Common.Util;
namespace ACM.Controllers

{
    public class IndexController : BaseController
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        PlatFormService platFormService = ServiceContext.getInstance().getPlatFormService();
        LoginService loginService = ServiceContext.getInstance().getLoginService();
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
        public ActionResult Login(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginCheck(UserModel model)
        {

            try
            {

                Session["SystemUser"] = loginService.Login(model.UserName, model.Pswd);

            }
            catch (SystemException ex)
            {
                return RedirectToAction("Login", "Index", new { msg = ex.Message });
            }
            return RedirectToAction("Index", "Index");
            
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModifyPswd(string data)
        {

            try
            {

                UserModel user = (UserModel) Session["SystemUser"];
        
                PswdModel pwd =  JsonConvert.JsonToObject<PswdModel>(data);
                loginService.ModifyPswd(user.UserName, pwd.OldPswd, pwd.NewPswd);
            }
            catch (SystemException ex)
            {
                return RedirectToAction("Index", "Index", new { msg = ex.Message });
            }
            return RedirectToAction("Login", "Index");

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

            //MenuTreeModel tree=platFormService.getMenuTreeByUserID(0);
            //return "[{id:\"user\", text: \"用户管理\"},{id:\"UserManage\", text:\"用户管理\", pid: \"user\" }]";
            MenuTreeModel tree = Stub.getTestTree();
            return tree.ToJsonStr();
        }
        public ActionResult Default()
        {
            return View();
        }

    }
}
