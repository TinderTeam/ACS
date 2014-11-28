using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Common.Util;
using ACS.Controllers.Constant;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Service;
using ACS.Test;

namespace ACS.Controllers
{
    public class DeptManageController : MiniUITableController<Dept>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        DeptService deptService = ServiceContext.getInstance().getDeptService();

        // GET: /DeptManage/
        //加载部门管理页面
        public ActionResult DeptManage()
        {
            return View();
        }
 
        //打开编辑部门窗口
        public ActionResult DeptEdit()
        {
            return View();
        }

  

        public override CommonService<Dept> getService()
        {
            return this.deptService;
        }
    }
}
