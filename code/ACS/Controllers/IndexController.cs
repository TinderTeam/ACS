﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Service;
using ACS.Models.Model;
using System.Resources;
using ACS.Common.Util;
using System.Web.Script.Serialization;
using ACS.Models.Po.Sys;
using ACS.Models.Po.CF;
using ACS.Common;
using ACS.Service.Constant;
namespace ACS.Controllers
{
    public class IndexController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        PlatFormService platFormService = ServiceContext.getInstance().getPlatFormService();
        LoginService loginService = ServiceContext.getInstance().getLoginService();

        //展示主界面
        public ActionResult Index()
        {
            return View();
        }
        //展示首页
        public ActionResult IndexPage()
        {
            return View();
        }
        //展示修改密码页面
        public ActionResult PasswordModify()
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
        /// 用户登陆
        /// </summary>

        public ActionResult LoginCheck(String data)
        {
            AjaxRspModel Rsp = new AjaxRspModel();
            log.Debug("User login, user Info is " + data);
            try
            {
                SystemUser loginUser = JsonConvert.JsonToObject<SystemUser>(data);
                Session["SystemUser"] = loginService.Login(loginUser);

            }
            catch (FuegoException e)
            {
                log.Error("cancel failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (SystemException e)
            {
                log.Error("cancel failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }
            return Content(JsonConvert.ObjectToJson(Rsp));

        }
        /// <summary>
        /// 修改密码
        public ActionResult ModifyPswd(string data)
        {
            AjaxRspModel Rsp = new AjaxRspModel();
            try
            {
                SystemUser user = (SystemUser)Session["SystemUser"];
                PswdModel pwd =  JsonConvert.JsonToObject<PswdModel>(data);
                loginService.ModifyPswd(user.UserName, pwd.OldPswd, pwd.NewPswd);
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (Exception e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }
            return Content(JsonConvert.ObjectToJson(Rsp));

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
        //加载左侧边栏，MenuTree
        public ActionResult MenuTree()
        {
            SystemUser loginUser = (SystemUser)Session["SystemUser"];
            List<Sys_Menu> sysMenuList = loginService.getSysMenuListByID(loginUser.UserID);
            //判断menu列表是否为空
            if (ValidatorUtil.isEmpty<Sys_Menu>(sysMenuList))
            {
                log.Warn("There is no Menu of this user. the user id is " + loginUser.UserID);
                return null;
            }
            List<TreeModel> menuTreeList = ModelConventService.toMenuTreeModelList(sysMenuList);

            return Content(JsonConvert.ObjectToJson(menuTreeList));
        }
        public ActionResult Default()
        {
            return View();
        }
        //密码类
        class PswdModel
        {
            public String OldPswd { get; set; }
            public String NewPswd { get; set; }
        }
    }
}
