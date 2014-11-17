﻿using ACS.Common.Util;
using ACS.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ACM.Controllers
{
    public abstract class BaseController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private AjaxRspModel rsp = new AjaxRspModel();

        public abstract void delete(string id);

        public AjaxRspModel Rsp
        {
            get { return rsp; }
            set { rsp = value; }
        }

        protected override void Initialize(RequestContext requestContext)
        {

           base.Initialize(requestContext);

            /* 
            if (null == Session["SystemUser"])
            {

                log.Warn("session is invalid");
                Response.Redirect("/");

            }
            */
            //然后验证写这里就有效啦，什么session 什么 RouteData 都能获取到了。谢谢大家，希望对后人又所帮助。
        }

        public String getRspJson()
        {
           return  JsonConvert.ObjectToJson(rsp);
        }
   


    }
}
