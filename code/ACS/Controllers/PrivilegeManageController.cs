using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Common.Model;
using ACS.Service;
using ACS.Test;
using ACS.Models.Po;
using System.Collections;
using PluSoft.Utils;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using ACS.Models.Po.CF;
namespace ACS.Controllers
{
    public class PrivilegeManageController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult PrivilegeManage()
        {
            return View();
        }

        public ActionResult Load(TableForm tableForm)
        {
            log.Debug("Load Data...");
            //数据库操作：使用查询条件、分页、排序等参数进行查询
            TableDataModel<Privilege> userModelTable = new TableDataModel<Privilege>();

            //userModelTable.setPage(tableForm.getPage());
            //userModelTable.setDataSource(userService.getUserList(filter));

            log.Debug("pageIndex = " + tableForm.PageIndex + ";pageSize=" + tableForm.PageSize);

            List<Privilege> list = Stub.getPrivilegeList();

            //返回JSON：将查询的结果，序列化为JSON字符串返回  
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            String json = jsonSerialize.Serialize(list);
            log.Debug("Json = " + json);
            Response.Write(json);
            return null;
        }
    }
}
