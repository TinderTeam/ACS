using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ACM.Controllers
{
    public class BaseController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

    }
}
