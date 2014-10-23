using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Common.Model;
using ACS.Service;
using ACS.Models.Po;
namespace ACM.Controllers
{
    public class UserManageController : Controller
    {
        UserService userService = ServiceContext.getInstance().getUserService();
        public ActionResult UserManage(TableForm tableForm, User filter)
        {
            TableDataModel<User> userModelTable = new TableDataModel<User>();
            userModelTable.setPage(tableForm.getPage());
            userModelTable.setDataSource(userService.getUserList(filter));

            @ViewBag.UserList = userModelTable.getCurrentPageData();
            return View();
        }

    }
}
